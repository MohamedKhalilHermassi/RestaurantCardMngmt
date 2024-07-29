using Abstraction;
using Model;

namespace Business
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
