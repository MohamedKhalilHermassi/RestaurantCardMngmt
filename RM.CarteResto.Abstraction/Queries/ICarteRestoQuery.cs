using RM.CarteResto.Model.Entitiy;

namespace RM.CarteResto.Abstraction.Queries
{
    public interface ICarteRestoQuery
    {
        Task<CarteRestaurant> getCardById(string partitionkey);
        Task<CarteRestaurant> getCardByUserId(string UserId);
        Task<IEnumerable<CarteRestaurant>> getAllCards();

    }
}
