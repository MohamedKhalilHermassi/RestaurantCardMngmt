using ProtoBuf.Grpc;
using System.ServiceModel;
using Google.Protobuf.WellKnownTypes;

namespace Remote
{
    [ServiceContract]
    public interface ITransactionServiceContract
    {
        [OperationContract]
        Task<TransactionByIdReply> getTransactionById(TransactionByIdRequest request, CallContext context = default);

        [OperationContract]
        Task<TransactionByIdReply> addTransaction(TransactionByIdReply transaction);

        [OperationContract]
        Task<Empty> RemoveTransaction(TransactionByIdRequest request, CallContext context = default);

        [OperationContract]
        Task<AllTransactionsReply> GetAllTransactions(Empty request, CallContext context = default);

        [OperationContract]
        Task<TransactionByIdReply> getTransactionByCardId(string id, CallContext context = default);
    }
}
