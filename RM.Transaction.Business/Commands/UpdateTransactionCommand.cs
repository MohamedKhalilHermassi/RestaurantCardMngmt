using RM.Transaction.Abstraction;
using RM.Transaction.Model;


namespace RM.Transaction.Business
{
    public class UpdateTransactionCommand
    {
        #region Fields
        private readonly ITransactionRepository _transactionRepository;

        #endregion
        #region Constructeur
        public UpdateTransactionCommand(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string partitionKey,Transactions transaction)
        {

            await _transactionRepository.UpdateTransaction(partitionKey,transaction);
        }
    }

}
