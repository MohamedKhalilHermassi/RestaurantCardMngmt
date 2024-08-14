using Moq;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;

namespace RM.CarteResto.UnitTests
{
    public class CommandTestFixture
    {
        public Mock<ICarteRestoRepository> MockRepository { get; private set; }
        public RemoveCardCommand RemoveCardCommand { get; private set; }

        public CommandTestFixture()
        {
            MockRepository = new Mock<ICarteRestoRepository>();
            RemoveCardCommand = new RemoveCardCommand(MockRepository.Object);
        }
    }

}
