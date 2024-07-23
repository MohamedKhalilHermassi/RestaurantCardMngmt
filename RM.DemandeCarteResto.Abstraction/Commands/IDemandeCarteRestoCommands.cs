using RM.DemandeCarteResto.Model.Entity;

namespace RM.DemandeCarteResto.Abstraction.Commands
{
    public interface IDemandeCarteRestoCommands
    {

        Task<DemandeCarteRestaurant> addDemandeCard(DemandeCarteRestaurant DemandeCarteResto);
        Task removeDemandeCard(string partitionkey);
        Task rejectDemandeCard(string partitionkey);
        Task acceptDemandeCard(string partitionkey);
        Task updateDemandeCard(string partitionkey, DemandeCarteRestaurant DemandeCarteRestaurant);

    }
}
