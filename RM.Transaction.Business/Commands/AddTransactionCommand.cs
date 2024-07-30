using RM.Transaction.Abstraction;
using RM.Transaction.Model;

namespace RM.Transaction.Business
{
    public class AddTransactionCommand
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddTransactionCommand(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transactions> ExecuteAsync(Transactions transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

           return await _transactionRepository.AddTransaction(transaction);
        }
    }
}
