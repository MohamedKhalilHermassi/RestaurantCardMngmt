using RM.DemandeCarteResto.Model.Entity;

namespace RM.DemandeCarteResto.Abstraction.Repositories
{ 
    public interface IDemandeCarteRestoRepository
    {
        Task<DemandeCarteRestaurant> addDemandeCarte(DemandeCarteRestaurant DemandeCarteResto);
        Task<DemandeCarteRestaurant> getDemandeCardById(string partitionkey);
        Task<IEnumerable<DemandeCarteRestaurant>> getDemandeCardByUserId(string UserId);
        Task<IEnumerable<DemandeCarteRestaurant>> getAllDemandes();
        Task<IEnumerable<DemandeCarteRestaurant>> getAllPendigDemandes();
        Task acceptDemandeCard(string partitionkey);
        Task removeDemandeCard(string partitionkey);
        Task rejectDemandeCard(string partitionkey);
        Task updateDemandeCard(string partitionkey, DemandeCarteRestaurant DemandeCarteRestaurant);
    }
}
