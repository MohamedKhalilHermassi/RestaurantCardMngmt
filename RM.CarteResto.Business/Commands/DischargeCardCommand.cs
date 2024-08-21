using RM.CarteResto.Abstraction;
using RM.Transaction.Remote;
using RM.Transaction.Service;

namespace RM.CarteResto.Business
{
    public class DischargeCardCommand
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepostiory;
        private readonly TransactionServiceGRPC _transactionService;
        #endregion

        #region Constructeur
        public DischargeCardCommand(ICarteRestoRepository carteRestoRepostiory, TransactionServiceGRPC transactionService)
        {
            _carteRestoRepostiory = carteRestoRepostiory;
            _transactionService = transactionService;
        } 
        #endregion
        public async Task ExecuteAsync(string partitionkey, float montant, string description)
        {

         

            var card = await _carteRestoRepostiory.GetCard(partitionkey);
            if (card == null)
            {
                throw new InvalidOperationException($"No card found for ID {partitionkey}");
            }
            if (card.Solde < montant)
            {
                throw new InvalidOperationException($"Card balance is insufficient {card.Solde} < {montant} ");

            }
            else
            {
                var trasnaction = new TransactionByIdReply
                {
                    CarteRestoId = partitionkey,
                    Description = description,
                    Montant = montant,
                    Type = false
                };
                await _transactionService.addTransaction(trasnaction);
                var IdTran = await _transactionService.getTransactionByCardId(partitionkey);

                var newTransactionIds = new string[card.TransactionIds.Length + 1];

                for (int i = 0; i < card.TransactionIds.Length; i++)
                {
                    newTransactionIds[i] = card.TransactionIds[i];
                }

                newTransactionIds[card.TransactionIds.Length] = IdTran.PartitionKey;

                card.TransactionIds = newTransactionIds;

                card.Solde -= montant;

                await _carteRestoRepostiory.UpdateCard(partitionkey, card);
            }
        }
    }
}
