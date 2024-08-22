using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProtoBuf.Grpc;
using RM.Transaction.Abstraction;
using RM.Transaction.Model;
using RM.Transaction.Remote;


namespace RM.Transaction.Service
{
    public class TransactionServiceGRPC : ITransactionServiceContract
    {
        #region Fields
        private readonly ITransactionRepository _transactionRepo;

        #endregion
        #region Constructeur
        public TransactionServiceGRPC(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        } 
        #endregion

        public async Task<TransactionByIdReply> addTransaction(TransactionByIdReply request)
        {
            var newTransaction = new Transactions
            {
                
                Date = DateTime.Now,
                Description = request.Description,
                Montant = request.Montant,
                Type = request.Type,
                CarteRestoId = request.CarteRestoId
               

            };

            var addedTransaction = await _transactionRepo.AddTransaction(newTransaction);

            var response = new TransactionByIdReply
            {
                Date = DateTime.Now,
                Description = addedTransaction.Description,
                Montant = addedTransaction.Montant,
                CarteRestoId = addedTransaction.CarteRestoId
            };
            return response;
        }

        public async Task<AllTransactionsReply> GetAllTransactions(Empty request, CallContext context = default)
        {
            var transactions = await _transactionRepo.GetAllTransactions();

            var response = new AllTransactionsReply
            {
                Transactions = transactions.Select(t => new TransactionByIdReply
                {
                    Id = t.Id,
                    Date = t.Date,
                    Description = t.Description ?? string.Empty,
                    Montant = t.Montant,
                    CarteRestoId = t.CarteRestoId
                })
            };

            return response;
        }

        public async Task<TransactionByIdReply> getTransactionById(TransactionByIdRequest request, CallContext context = default)
        {
            var transaction = await _transactionRepo.GetTransaction(request.partitionkey);

            if (transaction == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Transaction with ID {request.partitionkey} not found."));
            }

            var response = new TransactionByIdReply
            {
                Id = transaction.Id,
                Date = transaction.Date,
                Description = transaction.Description ?? string.Empty,
                Montant = transaction.Montant,
                CarteRestoId = transaction.CarteRestoId
            };

            return response;
        }

        public async Task<TransactionByIdReply> getTransactionByCardId(string id, CallContext context = default)
        {
            var transactions = await _transactionRepo.GetTransactionsByCardRestoId(id);

            if (transactions == null || !transactions.Any())
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No transactions found for card with ID {id}."));
            }

            var latestTransaction = transactions.OrderByDescending(t => t.Date).FirstOrDefault();

            if (latestTransaction == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No valid transactions found for card with ID {id}."));
            }

            var response = new TransactionByIdReply
            {
                Id = latestTransaction.Id,
                PartitionKey = latestTransaction.PartitionKey,
                Date = latestTransaction.Date,
                Description = latestTransaction.Description ?? string.Empty,
                Montant = latestTransaction.Montant,
                CarteRestoId = latestTransaction.CarteRestoId
            };

            return response;
        }

        public async Task<Empty> RemoveTransaction(TransactionByIdRequest request, CallContext context = default)
        {
            var transaction = await _transactionRepo.GetTransaction(request.partitionkey);
            if (transaction == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Transaction with ID {request.partitionkey} not found."));
            }

            await _transactionRepo.RemoveTransaction(request.partitionkey);
            return new Empty();
        }
    }
}
