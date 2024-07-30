using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
{
    public class GetCardQuery
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public GetCardQuery(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task<CarteRestaurant> ExecuteAsync(string partitionKey)
        {
            return await _carteRestoRepository.GetCard(partitionKey);
        }
    }
}
