using Google.Protobuf.WellKnownTypes;
using ProtoBuf.Grpc;
using System.ServiceModel;


namespace RM.CarteResto.Remote
{
    [ServiceContract]
    public interface ICarteRestoService
    {
        [OperationContract]
        Task<CarteRestoByIdReply> GetCarteRestoById(CarteRestoByIdRequest request, CallContext context = default);

        [OperationContract]
        Task<CarteRestoByIdReply> AddCarteResto(CarteRestoByIdReply carteResto);

        [OperationContract]
        Task<Empty> RemoveCarteResto(CarteRestoByIdRequest request, CallContext context = default);
        [OperationContract]
        Task<AllCartesRestoReply> GetAllCarteResto(Empty request, CallContext context = default);
        [OperationContract]
        Task<bool> VerifyCarteRestoSolde(string id,float montant,CallContext context = default);
    }
}
