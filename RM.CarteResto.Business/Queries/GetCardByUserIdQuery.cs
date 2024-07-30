using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class GetCardByUserIdQuery
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public GetCardByUserIdQuery(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task<CarteRestaurant> ExecuteAsync(string partitionKey)
        {
            return await _carteRestoRepository.GetCardByUserId(partitionKey);
        }
    }
}
