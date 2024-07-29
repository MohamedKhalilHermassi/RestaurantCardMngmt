using Abstraction;
using Model;

namespace Business
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
