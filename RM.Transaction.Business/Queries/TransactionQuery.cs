using RM.Transaction.Abstraction.Queries;
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Model.Entity;

namespace RM.Transaction.Business.Queries
{
    public class TransactionQuery : ITransactionQuery
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionQuery(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public Task<IEnumerable<Transactions>> getAllTransactions()
        {
        return transactionRepository.getAllTransactions();
        }

        public Task<Transactions> getTransactionById(string partitionkey)
        {
            return transactionRepository.getTransactionById(partitionkey); 
        }

        public Task<IEnumerable<Transactions>> getTransactionsByCardRestoId(string cardRestoId)
        {
            return transactionRepository.getTransactionsByCardRestoId(cardRestoId);
        }
    }
}
