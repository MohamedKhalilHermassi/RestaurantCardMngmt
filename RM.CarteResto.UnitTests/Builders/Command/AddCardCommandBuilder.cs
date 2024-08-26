using Moq;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;
using RM.CarteResto.Model;

namespace RM.CarteResto.UnitTests
{
    public sealed class AddCardCommandBuilder
    {

        public readonly Mock<ICarteRestoRepository> CarteRestoRepositoryMock = new Mock<ICarteRestoRepository>();
        private readonly CarteRestaurantStore carterestaurantStore = new CarteRestaurantStore();

        private readonly CarteRestaurant _carteRestaurant;

        public AddCardCommandBuilder()
        {
            CarteRestaurantStore carteRestaurantStore = new CarteRestaurantStore();
            CarteRestaurant _carteRestaurant = carteRestaurantStore.validCarteRestaurant();
        }

        public AddCardCommandBuilder WithPartitionKey()
        {
            _carteRestaurant.PartitionKey = Values.PARTITIONKEY;
            return this;
        }

        public AddCardCommandBuilder WithNumero()
        {
            _carteRestaurant.Numero = Values.NUMERO;
            return this;
        }
        public AddCardCommandBuilder WithSolde()
        {
            _carteRestaurant.Solde = Values.SOLDE;
            return this;
        }

        public AddCardCommand Build()
        {

            CarteRestoRepositoryMock.Setup(r => r.AddCard(It.IsAny<CarteRestaurant>()))
                .ReturnsAsync(_carteRestaurant);

            return new AddCardCommand(CarteRestoRepositoryMock.Object);
        }

    }
}
