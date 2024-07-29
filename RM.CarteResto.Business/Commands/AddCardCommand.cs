using Abstraction;
using Model;

namespace Business
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
