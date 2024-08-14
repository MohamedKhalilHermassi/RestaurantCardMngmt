using Moq;
using RM.CarteResto.Abstraction;

namespace RM.CarteResto.UnitTests.CommandsUnitTests
{
    public class CommandAssertions
    {
        private readonly Mock<ICarteRestoRepository> _mockRepository;

        public CommandAssertions(Mock<ICarteRestoRepository> mockRepository)
        {
            _mockRepository = mockRepository;
        }

        public void VerifyRemoveCardCalledOnce(string partitionKey)
        {
            _mockRepository.Verify(r => r.RemoveCard(partitionKey), Times.Once);
        }
    }

}
