using NotificationsClient;
using RM.CarteResto.Abstraction;
using RM.Notif.Business.Commands;
using RM.Transaction.Remote;
using RM.Transaction.Service;

namespace RM.CarteResto.Business
{
    public class ChargeCardCommand
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;
        private readonly TransactionServiceGRPC _transactionService;
        private readonly RechargeCardEmail _rechargeCardEmail;
        private readonly ClientSignalR _clientSignalR;
        #endregion

        #region Constructeur
        public ChargeCardCommand(ICarteRestoRepository carteRestoRepository, TransactionServiceGRPC transactionService, RechargeCardEmail rechargeCardEmail, ClientSignalR clientSignalR)
        {
            _carteRestoRepository = carteRestoRepository;
            _transactionService = transactionService;
            _rechargeCardEmail = rechargeCardEmail;
            _clientSignalR = clientSignalR;
        } 
        #endregion

        public async Task ExecuteAsync(string partitionkey, float montant)
        {
            var trasnaction = new TransactionByIdReply
            {
                Id = Guid.Parse(partitionkey),
                CarteRestoId = partitionkey,
                Description = "Recharge de la carte restaurant ",
                Montant = montant,
                Type = true
            };
            await _transactionService.addTransaction(trasnaction);
            
            var IdTran = await _transactionService.getTransactionByCardId(partitionkey);
           
            var card = await _carteRestoRepository.GetCard(partitionkey);

            if (card == null)
            {
                throw new InvalidOperationException($"No card found for ID {partitionkey}");
            }
            // EMAIL 
            await _rechargeCardEmail.ExecuteAsync(card.UserEmail);
            // SIGNAL R NOTIF
            await _clientSignalR.AlertEmployeeChargedCard(card.UserEmail);

            var newTransactionIds = new string[card.TransactionIds.Length + 1];

            for (int i = 0; i < card.TransactionIds.Length; i++)
            {
                newTransactionIds[i] = card.TransactionIds[i];
            }

            newTransactionIds[card.TransactionIds.Length] = IdTran.PartitionKey;

            card.TransactionIds = newTransactionIds;

            card.Solde += montant;

            await _carteRestoRepository.UpdateCard(partitionkey, card);
        }
    }
}
