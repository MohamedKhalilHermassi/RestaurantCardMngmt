using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class AddCardCommand
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;
        #endregion

        #region Constructeur
        public AddCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        } 
        #endregion
        public async Task ExecuteAsync(CarteRestaurant carte)
        {
            await _carteRestoRepository.AddCard(carte);
        }
    }
}
