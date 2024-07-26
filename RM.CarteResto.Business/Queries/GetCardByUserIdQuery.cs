using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Model.Entitiy;

namespace RM.CarteResto.Business.Queries
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
