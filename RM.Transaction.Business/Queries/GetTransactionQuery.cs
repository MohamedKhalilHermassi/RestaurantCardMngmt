using Abstraction;
using Model;


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
