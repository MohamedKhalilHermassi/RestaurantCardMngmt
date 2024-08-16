using Moq;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.UnitTests
{
    public sealed class AddCardCommandBuilder
    {
        readonly Mock<ICarteRestoRepository> CarteRestoRepositoryMock;
        private CarteRestaurant _carteRestaurant;


    }
}
