using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class AddCardCommand
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public AddCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task ExecuteAsync(CarteRestaurant carte)
        {
            await _carteRestoRepository.AddCard(carte);
        }
    }
}
