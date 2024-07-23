using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProtoBuf.Grpc;
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Model.Entity;
using RM.Transaction.Remote.Contracts;


namespace RM.Transaction.Service.Services
{
    public class TransactionServiceGRPC : ITransactionServiceContract
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionServiceGRPC(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<TransactionByIdReply> addTransaction(TransactionByIdReply request)
        {
            var newTransaction = new Transactions
            {
                Id = request.Id,
                Date = DateTime.Now,
                Description = request.Description,
                Montant = request.Montant,
                Type = request.Type,
                CarteRestoId = request.CarteRestoId
                

            };

            var addedTransaction = await _transactionRepo.addTransaction(newTransaction);

            var response = new TransactionByIdReply
            {
                Id = addedTransaction.Id,
                Date = DateTime.Now,
                Description = addedTransaction.Description,
                Montant = addedTransaction.Montant,
                CarteRestoId = addedTransaction.CarteRestoId
            };
            return response;
        }

        public async Task<AllTransactionsReply> GetAllTransactions(Empty request, CallContext context = default)
        {
            var transactions = await _transactionRepo.getAllTransactions();

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
            var transaction = await _transactionRepo.getTransactionById(request.partitionkey);

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
            var transactions = await _transactionRepo.getTransactionsByCardRestoId(id);

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
            var transaction = await _transactionRepo.getTransactionById(request.partitionkey);
            if (transaction == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Transaction with ID {request.partitionkey} not found."));
            }

            await _transactionRepo.removeTransaction(request.partitionkey);
            return new Empty();
        }
    }
}
