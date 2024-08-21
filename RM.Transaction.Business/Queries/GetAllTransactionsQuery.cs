using RM.Transaction.Abstraction;
using RM.Transaction.Model;


namespace RM.Transaction.Business
{
    public class GetAllTransactionsQuery
    {
        #region Fields
        private readonly ITransactionRepository _transactionRepository;

        #endregion
        #region Constructeur
        public GetAllTransactionsQuery(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 
        #endregion

        public async Task<IEnumerable<Transactions>> ExecuteAsync()
        {
            return await _transactionRepository.GetAllTransactions();
        }

    }
}
