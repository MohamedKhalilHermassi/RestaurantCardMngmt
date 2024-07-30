using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class UpdateCardCommand
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public UpdateCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task ExecuteAsync(string partitionKey, CarteRestaurant card)
        {
            await _carteRestoRepository.UpdateCard(partitionKey, card); 
        }
    }
}
