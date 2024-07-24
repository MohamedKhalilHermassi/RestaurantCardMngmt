
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Model.Entity;

namespace Business
{
    public class GetTransactionsByCardQuery
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionsByCardQuery(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transactions>> ExecuteAsync(string cardId)
        {
            return await _transactionRepository.GetTransactionsByCardRestoId(cardId);
        }

    }
}
