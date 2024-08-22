using RM.Transaction.Abstraction;


namespace RM.Transaction.Business
{
    public class RemoveTransactionCommand
    {
        #region Fields
        private readonly ITransactionRepository _transactionRepository;

        #endregion
        #region Constructeur
        public RemoveTransactionCommand(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string partitionKey)
        {
            await _transactionRepository.RemoveTransaction(partitionKey);
        }
    }
}
