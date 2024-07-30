using RM.CarteResto.Model;

namespace RM.CarteResto.Abstraction
{
    public interface ICarteRestoRepository
    {
        Task<CarteRestaurant> AddCard(CarteRestaurant carteResto);
        Task<CarteRestaurant> GetCardByUserId(string UserId);
        Task<CarteRestaurant> GetCard(string partitionKey);
        Task<IEnumerable<CarteRestaurant>> GetAllCards();
        Task RemoveCard(string partitionkey);

        Task UpdateCard(string partitionkey, CarteRestaurant card);
    }
}
