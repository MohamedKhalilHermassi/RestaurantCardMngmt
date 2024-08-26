using Moq;
using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Business;
using RM.DemandeCarteResto.Model;
namespace RM.DemandeCarteResto.UnitTests
{
    public sealed class AddDemandeCarteRestoBuilder
    {

        public readonly Mock<IDemandeCarteRestoRepository> demandeCarteRestoRepositoryMock = new Mock<IDemandeCarteRestoRepository>();
        private readonly DemandeCarteRestaurant carterestaurantStore = new DemandeCarteRestaurant();
        private readonly DemandeCarteRestaurant demandeCarteRestaurant;

        public AddDemandeCarteRestoBuilder()
        {
            DemandeCarteRestaurantStore demandeCarteRestaurantStore = new DemandeCarteRestaurantStore();
            demandeCarteRestaurant = demandeCarteRestaurantStore.DemandeCarteValid();
        }

        public AddDemandeCarteRestoBuilder WithPartitionKey()
        {
            demandeCarteRestaurant.PartitionKey = Values.PARTITION_KEY;
            return this;
        }

        public AddDemandeCarteRestoBuilder WithStatus()
        {
            demandeCarteRestaurant.Status = Values.Status;
            return this;
        }
        public AddDemandeCarteRestoBuilder WithDate()
        {
            demandeCarteRestaurant.Date = Values.DATE;
            return this;
        }

        public AddDemandCardCommand Build()
        {

            demandeCarteRestoRepositoryMock.Setup(r => r.AddDemandeCarte(It.IsAny<DemandeCarteRestaurant>()))
                .ReturnsAsync(demandeCarteRestaurant);

            return new AddDemandCardCommand(demandeCarteRestoRepositoryMock.Object);
        }
    }
}
