using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Model.Entity;


namespace Business
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
