using Google.Protobuf.WellKnownTypes;
using Moq;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;
using RM.CarteResto.Model;

namespace RM.CarteResto.UnitTests.Builders.Command
{
    public sealed class RemoveCardCommandBuilder
    {
        public readonly Mock<ICarteRestoRepository> CarteRestoRepositoryMock;
        private CarteRestaurant _carteRestaurant;

        public RemoveCardCommandBuilder()
        {

            var carteRestaurantStore = new CarteRestaurantStore();
            CarteRestoRepositoryMock = new Mock<ICarteRestoRepository>();
            _carteRestaurant = carteRestaurantStore.validCarteRestaurant();
        }

        public RemoveCardCommandBuilder WithPartitionKey()
        {
            _carteRestaurant.PartitionKey = Values.PARTITIONKEY;
            return this;
        }

        public RemoveCardCommand Build()
        {

            CarteRestoRepositoryMock.Setup(r => r.RemoveCard(It.IsAny<string>()));
            return new RemoveCardCommand(CarteRestoRepositoryMock.Object);
        }

    }
}
