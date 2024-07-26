using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Model.Entitiy;

namespace RM.CarteResto.Business.Queries
{
    public class GetAllCardsQuery
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public GetAllCardsQuery(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task<IEnumerable<CarteRestaurant>> ExecuteAsync()
        {
            return await _carteRestoRepository.GetAllCards();
        }
    }
}
