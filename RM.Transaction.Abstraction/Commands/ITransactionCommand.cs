using RM.Transaction.Model.Entity;

namespace RM.Transaction.Abstraction.Commands
{
    public interface ITransactionCommand
    {
        Task<Transactions> addTransaction(Transactions transaction);
        Task removeTransaction(string partitionkey);
        Task updateTransaction(string partitionkey, Transactions transaction);
    }
}
