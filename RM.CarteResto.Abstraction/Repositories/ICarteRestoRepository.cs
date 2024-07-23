using RM.CarteResto.Model.Entitiy;


namespace RM.CarteResto.Abstraction.Repositories
{
    public interface ICarteRestoRepository
    {
        Task<CarteRestaurant> addCard(CarteRestaurant carteResto);
        Task<CarteRestaurant> getCardByUserId(string UserId);
        Task<CarteRestaurant> getCardById(string partitionkey);
        Task<IEnumerable<CarteRestaurant>> getAllCards();
        Task removeCard(string partitionkey);
        Task ChargeCard(string partitionkey, float montant, string IdTransaction);
        Task DischargeCard(string partitionkey, float montant, string IdTransaction);

        Task updateCard(string partitionkey, CarteRestaurant card);
        Task decrementCardSolde(string partitionkey, float montant);
    }
}
