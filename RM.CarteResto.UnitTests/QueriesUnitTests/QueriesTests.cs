using Moq;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;
using RM.CarteResto.Model;
using Xunit;

namespace RM.CarteResto.UnitTests.QueriesUnitTests
{
    public class QueriesTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnCard_WhenCardExists()
        {
            // Arrange
            var mockRepository = new Mock<ICarteRestoRepository>();
            var partitionKey = Guid.NewGuid();
            var expectedCard = new CarteRestaurant
            {
                Id = partitionKey,
                Solde = 100.0f,
                TransactionIds = new string[0]
            };

            mockRepository.Setup(repo => repo.GetCard(partitionKey.ToString()))
                .ReturnsAsync(expectedCard);

            var getCardQuery = new GetCardQuery(mockRepository.Object);

            // Act
            var result = await getCardQuery.ExecuteAsync(partitionKey.ToString()    );

            // Assert
            Assert.Equal(expectedCard, result);
            mockRepository.Verify(r => r.GetCard(partitionKey.ToString()), Times.Once);
        }
    }
}
