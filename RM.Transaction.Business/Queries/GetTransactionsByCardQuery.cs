using RM.Transaction.Abstraction;
using RM.Transaction.Model;

namespace RM.Transaction.Business
{
    public class GetTransactionsByCardQuery
    {
        #region Fields
        private readonly ITransactionRepository _transactionRepository;

        #endregion
        #region Constructeur
        public GetTransactionsByCardQuery(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        } 
        #endregion

        public async Task<IEnumerable<Transactions>> ExecuteAsync(string cardId)
        {
            return await _transactionRepository.GetTransactionsByCardRestoId(cardId);
        }

    }
}
