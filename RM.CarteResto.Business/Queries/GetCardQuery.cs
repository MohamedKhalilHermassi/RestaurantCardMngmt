using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class GetCardQuery
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;

        #endregion
        #region Constructeur
        public GetCardQuery(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        } 
        #endregion
        public async Task<CarteRestaurant> ExecuteAsync(string partitionKey)
        {
            return await _carteRestoRepository.GetCard(partitionKey);
        }
    }
}
