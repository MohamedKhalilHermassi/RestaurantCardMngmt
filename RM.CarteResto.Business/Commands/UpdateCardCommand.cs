using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class UpdateCardCommand
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;

        #endregion
        #region Constructeur
        public UpdateCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        } 
        #endregion
        public async Task ExecuteAsync(string partitionKey, CarteRestaurant card)
        {
            await _carteRestoRepository.UpdateCard(partitionKey, card); 
        }
    }
}
