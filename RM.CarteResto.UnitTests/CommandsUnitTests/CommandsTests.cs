using Moq;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;
using RM.CarteResto.Model;
using Xunit;

namespace RM.CarteResto.Tests
{
    public class CommandsTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldCallAddCardOnRepository()
        {
            var guid = Guid.NewGuid();
            // Arrange
            var mockRepository = new Mock<ICarteRestoRepository>();
            var addCardCommand = new AddCardCommand(mockRepository.Object);
            var testCard = new CarteRestaurant
            {
                Id = guid,
                Numero ="12345678",
                PartitionKey = guid.ToString(),
                Solde=150,
                UserEmail="test@test.com",
                UserId=Guid.NewGuid().ToString(),
                TransactionIds = [Guid.NewGuid().ToString(),Guid.NewGuid().ToString()]
               
            };

            // Act
            await addCardCommand.ExecuteAsync(testCard);

            // Assert
            mockRepository.Verify(r => r.AddCard(It.Is<CarteRestaurant>(c => c.Id == testCard.Id && c.Solde == testCard.Solde && c.UserEmail == testCard.UserEmail)), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCallRemoveCardOnRepository()
        {
            // Arrange
            var mockRepository = new Mock<ICarteRestoRepository>();
            var removeCardCommand = new RemoveCardCommand(mockRepository.Object);
            var partitionKey = "12345678";

            // Act
            await removeCardCommand.ExecuteAsync(partitionKey);

            // Assert
            mockRepository.Verify(r => r.RemoveCard(partitionKey), Times.Once);
        }

    }
}
