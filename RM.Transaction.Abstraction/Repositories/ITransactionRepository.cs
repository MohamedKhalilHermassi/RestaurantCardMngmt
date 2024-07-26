using RM.Transaction.Model.Entity;

namespace RM.Transaction.Abstraction.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transactions> AddTransaction(Transactions transaction);
        Task<Transactions> GetTransaction(string partitionkey);
        Task<IEnumerable<Transactions>> GetTransactionsByCardRestoId(string cardRestoId);

        Task<IEnumerable<Transactions>> GetAllTransactions();
        Task RemoveTransaction(string partitionkey);
        Task UpdateTransaction(string partitionkey, Transactions transaction);
    }
}
