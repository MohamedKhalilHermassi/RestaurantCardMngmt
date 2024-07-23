using RM.Transaction.Model.Entity;

namespace RM.Transaction.Abstraction.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transactions> addTransaction(Transactions transaction);
        Task<Transactions> getTransactionById(string transactionId);
        Task<IEnumerable<Transactions>> getTransactionsByCardRestoId(string cardRestoId);

        Task<IEnumerable<Transactions>> getAllTransactions();
        Task removeTransaction(string partitionkey);
        Task updateTransaction(string partitionkey, Transactions transaction);
    }
}
