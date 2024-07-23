using RM.Transaction.Model.Entity;

namespace RM.Transaction.Abstraction.Queries
{
    public interface ITransactionQuery
    {
        Task<Transactions> getTransactionById(string partitonKey);
        Task<IEnumerable<Transactions>> getAllTransactions();
        Task<IEnumerable<Transactions>> getTransactionsByCardRestoId(string cardRestoId);
    }
}
