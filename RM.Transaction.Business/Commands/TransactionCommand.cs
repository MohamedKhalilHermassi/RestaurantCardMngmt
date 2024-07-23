using RM.Transaction.Abstraction.Commands;
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Model.Entity;


namespace RM.Transaction.Business.Commands
{
    public class TransactionCommand : ITransactionCommand
    {

        private readonly ITransactionRepository transactionRepository;

        public TransactionCommand(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }


        public Task<Transactions> addTransaction(Transactions transaction)
        {
               return  transactionRepository.addTransaction(transaction);
         }

        public Task removeTransaction(string partitionkey)
        {
          return   transactionRepository.removeTransaction(partitionkey);
        }

        public Task updateTransaction(string partitionkey, Transactions transaction)
        {
            return transactionRepository.updateTransaction(partitionkey, transaction);
        }
    }
}
