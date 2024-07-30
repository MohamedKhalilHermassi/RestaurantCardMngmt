using RM.Transaction.Abstraction;


namespace RM.Transaction.Business
{
    public class RemoveTransactionCommand
    {
        private readonly ITransactionRepository _transactionRepository;

        public RemoveTransactionCommand(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task ExecuteAsync(string partitionKey)
        {
            await _transactionRepository.RemoveTransaction(partitionKey);
        }
    }
}
