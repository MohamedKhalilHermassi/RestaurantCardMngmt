using RM.Transaction.Abstraction;
using RM.Transaction.Model;


namespace RM.Transaction.Business
{
    public class GetTransactionQuery
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionQuery(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transactions> ExecuteAsync(string partitionKey)
        {
            return await _transactionRepository.GetTransaction(partitionKey);
        }
    }
}
