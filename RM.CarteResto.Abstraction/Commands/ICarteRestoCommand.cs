using RM.CarteResto.Model.Entitiy;

namespace RM.CarteResto.Abstraction.Commands
{
    public interface ICarteRestoCommand
    {
        Task removeCard(string partitionkey);
        Task<CarteRestaurant> addCard(CarteRestaurant carteResto);

        Task updateCard(string partitionkey, CarteRestaurant card);
        Task ChargeCard(string partitionkey, float montant, string IdTransaction);
        Task DischargeCard(string partitionkey, float montant, string IdTransaction);

    }
}
