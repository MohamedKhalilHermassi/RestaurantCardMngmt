using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class GetAllCardsQuery
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;

        #endregion
        #region Constructeur
        public GetAllCardsQuery(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        } 
        #endregion
        public async Task<IEnumerable<CarteRestaurant>> ExecuteAsync()
        {
            return await _carteRestoRepository.GetAllCards();
        }
    }
}
