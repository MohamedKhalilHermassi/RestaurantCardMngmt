using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Model.Entitiy;

namespace RM.CarteResto.Business.Queries
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
