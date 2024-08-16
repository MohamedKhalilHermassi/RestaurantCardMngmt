using Moq;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;
using RM.CarteResto.Model;

namespace RM.CarteResto.UnitTests
{
    public sealed class GetCardByUserIdQueryBuilder
    {
         readonly Mock<ICarteRestoRepository> CarteRestoRepositoryMock;
        private CarteRestaurant _carteRestaurant;

        public GetCardByUserIdQueryBuilder()
        {
            var carteRestaurantStore = new CarteRestaurantStore();

            CarteRestoRepositoryMock = new Mock<ICarteRestoRepository>();
            _carteRestaurant = carteRestaurantStore.validCarteRestaurant();
        }

        public GetCardByUserIdQueryBuilder WithUserId()
        {
            _carteRestaurant.UserId = Values.USER_ID;
            return this;
        }

        public GetCardByUserIdQueryBuilder WithPartitionKey()
        {
            _carteRestaurant.PartitionKey = Values.PARTITIONKEY;
            return this;
        }

       


        public GetCardByUserIdQuery Build()
        {

            CarteRestoRepositoryMock.Setup(r => r.GetCardByUserId(It.IsAny<string>()))
                .ReturnsAsync(_carteRestaurant);

            return new GetCardByUserIdQuery(CarteRestoRepositoryMock.Object);
        }
    }
}
