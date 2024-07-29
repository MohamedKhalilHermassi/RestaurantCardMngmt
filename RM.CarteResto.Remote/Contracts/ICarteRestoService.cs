using Google.Protobuf.WellKnownTypes;
using ProtoBuf.Grpc;
using System.ServiceModel;


namespace Remote
{
    [ServiceContract]
    public interface ICarteRestoService
    {
        [OperationContract]
        Task<CarteRestoByIdReply> getCarteRestoById(CarteRestoByIdRequest request, CallContext context = default);

        [OperationContract]
        Task<CarteRestoByIdReply> addCarteResto(CarteRestoByIdReply carteResto);

        [OperationContract]
        Task<Empty> removeCarteResto(CarteRestoByIdRequest request, CallContext context = default);
        [OperationContract]
        Task<AllCartesRestoReply> GetAllCarteResto(Empty request, CallContext context = default);
        [OperationContract]
        Task<bool> verifyCarteRestoSolde(string id,float montant,CallContext context = default);
    }
}
