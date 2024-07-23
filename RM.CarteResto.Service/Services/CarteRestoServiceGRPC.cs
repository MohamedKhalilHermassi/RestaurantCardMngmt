using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProtoBuf.Grpc;
using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Model.Entitiy;
using RM.CarteResto.Remote.Contracts;

namespace RM.CarteResto.Service.Services
{
    public class CarteRestoServiceGRPC : ICarteRestoService
    {
        private readonly ICarteRestoRepository _carteRepo;

        public CarteRestoServiceGRPC(ICarteRestoRepository carteRepo)
        {
            _carteRepo = carteRepo;
        }

        public async Task<CarteRestoByIdReply> addCarteResto(CarteRestoByIdReply carteResto)
        {

            var card = new CarteRestaurant
            {
                Id = Guid.NewGuid(),
                Numero = carteResto.Numero,
                Solde = carteResto.Solde,
                TransactionIds = carteResto.TransactionIds,
                UserId = carteResto.UserId,
                UserEmail = carteResto.UserEmail,
            };
            var addedCard = await _carteRepo.addCard(card);
            var reply = new CarteRestoByIdReply
            {
                Id = Guid.NewGuid(),
                Numero = addedCard.Numero,
                Solde = addedCard.Solde,
                TransactionIds = addedCard.TransactionIds,
                UserId = addedCard.UserId,
                UserEmail = addedCard.UserEmail
            };
            return reply;

        }
        public async Task<AllCartesRestoReply> GetAllCarteResto(Empty request, CallContext context = default)
        {
            var cards = await _carteRepo.getAllCards();

            var response = new AllCartesRestoReply
            {
                CartesRestaurant = cards.Select(t => new CarteRestoByIdReply
                {
                    Numero = t.Numero,
                    Solde = t.Solde,
                    TransactionIds = t.TransactionIds
                })
            };

            return response;
        }

        public async Task<CarteRestoByIdReply> getCarteRestoById(CarteRestoByIdRequest request, CallContext context = default)
        {
            var card = await _carteRepo.getCardById(request.PartitionKey);

            if (card == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Card with ID {request.PartitionKey} not found."));
            }

            var response = new CarteRestoByIdReply
            {
                Id = Guid.NewGuid(),
                Numero = card.Numero,
                Solde = card.Solde,
                TransactionIds = card.TransactionIds,
              
            };

            return response;
        }

        public async Task<Empty> removeCarteResto(CarteRestoByIdRequest request, CallContext context = default)
        {
            var card = await _carteRepo.getCardById(request.PartitionKey);
            if (card == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Card with ID {request.PartitionKey} not found."));
            }

            await _carteRepo.removeCard(request.PartitionKey);
            return new Empty();
        }

        public async  Task<bool> verifyCarteRestoSolde(string id,float montant, CallContext context = default)
        {
            var carte = new CarteRestoByIdRequest
            {
                PartitionKey = id
            };
           var carteResponse = await getCarteRestoById(carte);
            if(carteResponse.Solde< montant)
            {
                return false;
            }
        
            await _carteRepo.decrementCardSolde(id, montant);
            return true;
        }
    }
}
