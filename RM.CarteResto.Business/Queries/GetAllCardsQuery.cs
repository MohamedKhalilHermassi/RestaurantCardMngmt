using RM.CarteResto.Abstraction;
using RM.CarteResto.Model;

namespace RM.CarteResto.Business
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
