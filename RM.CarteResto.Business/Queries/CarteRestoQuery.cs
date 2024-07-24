using RM.CarteResto.Abstraction.Queries;
using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Model.Entitiy;

namespace RM.CarteResto.Business.Queries
{
    public class CarteRestoQuery : ICarteRestoQuery
    {
        private readonly ICarteRestoRepository _carteRepo;

        public CarteRestoQuery(ICarteRestoRepository carteRepo)
        {
            _carteRepo = carteRepo;
        }

        public async Task<IEnumerable<CarteRestaurant>> getAllCards()
        {
        return await _carteRepo.GetAllCards();
        }

        public async Task<CarteRestaurant> getCardById(string partitionkey)
        {
        return await _carteRepo.GetCard(partitionkey);
        }

        public async Task<CarteRestaurant> getCardByUserId(string UserId)
        {
            return await _carteRepo.GetCardByUserId(UserId);
        }
    }
}
