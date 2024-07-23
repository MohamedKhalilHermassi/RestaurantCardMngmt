using Microsoft.EntityFrameworkCore;
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Data.Data;
using RM.Transaction.Model.Entity;
using System.Collections.Concurrent;

namespace RM.Transaction.Business
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionContext _context;

        public TransactionRepository(TransactionContext context)
        {
           _context = context;
        }

        public async Task<Transactions> addTransaction(Transactions transaction)
        {
            var result =   await _context.Transactions.AddAsync(transaction);
            
            await _context.SaveChangesAsync();
            return result.Entity;
                
        }

        public async Task<IEnumerable<Transactions>> getAllTransactions()
        {
            return  _context.Transactions.AsEnumerable();
        }

        public async Task<Transactions> getTransactionById(string PartitionId)
        {
            
            return await _context.Transactions.FindAsync(PartitionId);
        }

        public async Task<IEnumerable<Transactions>> getTransactionsByCardRestoId(string cardRestoId)
        {
            return await _context.Transactions.Where(t => t.CarteRestoId == cardRestoId).ToListAsync();
        }

        public async Task removeTransaction(string partitionkey)
        {
            var transaction = await _context.Transactions.FindAsync(partitionkey);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction with ID {partitionkey} not found");
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

        }

        public async Task  updateTransaction(string partitionkey, Transactions transaction)
        {
            if (partitionkey != transaction.PartitionKey)
            {
                throw new ArgumentException("Transaction ID mismatch");
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
              if (!TransactionExists(partitionkey))
                {
                    throw new KeyNotFoundException($"Transaction with ID {partitionkey} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool TransactionExists(string partitionkey)
        {
            return _context.Transactions.Any(e => e.PartitionKey == partitionkey);
        }

        
    }
}
