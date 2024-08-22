using RM.Transaction.Abstraction;
using RM.Transaction.Model;


namespace RM.Transaction.Business
{
    public class GetTransactionQuery
    {
        #region Fields
        private readonly ITransactionRepository _transactionRepository;

        #endregion
        #region Constructeur
        public GetTransactionQuery(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 
        #endregion

        public async Task<Transactions> ExecuteAsync(string partitionKey)
        {
            return await _transactionRepository.GetTransaction(partitionKey);
        }
    }
}
