using RM.CarteResto.UnitTests;
using RM.CarteResto.UnitTests.CommandsUnitTests;
using Xunit;

namespace RM.CarteResto.Tests
{
    public class CommandsTests
    {
        public class RemoveCardCommandTests
        {
            private readonly CommandTestFixture _fixture;

            public RemoveCardCommandTests()
            {
                _fixture = new CommandTestFixture();
            }

            [Fact]
            public async Task ExecuteAsync_ShouldCallRemoveCardOnRepository()
            {
                // Arrange
                var partitionKey = "12345678";

                // Act
                await CommandExecutor.ExecuteAsync(_fixture.RemoveCardCommand, partitionKey);

                // Assert
                var assertions = new CommandAssertions(_fixture.MockRepository);
                assertions.VerifyRemoveCardCalledOnce(partitionKey);
            }
        }


    }
}
