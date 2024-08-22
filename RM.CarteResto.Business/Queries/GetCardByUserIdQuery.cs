using Microsoft.Azure.Cosmos;
using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class GetCardByUserIdQuery
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;

        #endregion
        #region Constructeur
        public GetCardByUserIdQuery(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        } 
        #endregion
        public async Task<CarteRestaurant> ExecuteAsync(string partitionKey)
        {
            if (string.IsNullOrEmpty(partitionKey))
            {
                throw new ArgumentException("UserId cannot be null or empty", nameof(partitionKey));
            }

            return await _carteRestoRepository.GetCardByUserId(partitionKey);
        }
    }
}
