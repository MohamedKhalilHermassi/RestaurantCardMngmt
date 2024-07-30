using RM.Transaction.Abstraction;
using RM.Transaction.Model;


namespace RM.Transaction.Business
{
    public class GetAllTransactionsQuery
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetAllTransactionsQuery(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transactions>> ExecuteAsync()
        {
            return await _transactionRepository.GetAllTransactions();
        }

    }
}
