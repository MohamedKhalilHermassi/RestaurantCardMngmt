using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RM.CarteResto.Abstraction.Repositories;
using RM.Transaction.Remote.Contracts;
using RM.Transaction.Service.Services;

namespace RM.CarteResto.Business.Commands
{
    public class DischargeCardCommand
    {
        private readonly ICarteRestoRepository _carteRestoRepostiory;
        private readonly TransactionServiceGRPC _transactionService;

        public DischargeCardCommand(ICarteRestoRepository carteRestoRepostiory, TransactionServiceGRPC transactionService)
        {
            _carteRestoRepostiory = carteRestoRepostiory;
            _transactionService = transactionService;
        }
        public async Task ExecuteAsync(string partitionkey, float montant, string description)
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

            var card = await _carteRestoRepostiory.GetCard(partitionkey);
            if (card == null)
            {
                throw new InvalidOperationException($"No card found for ID {partitionkey}");
            }

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
