using Model;

namespace Abstraction
{
    public interface IDemandeCarteRestoRepository
    {
        Task<DemandeCarteRestaurant> AddDemandeCarte(DemandeCarteRestaurant DemandeCarteResto);
        Task<DemandeCarteRestaurant> GetDemandeCardById(string partitionkey);
        Task<IEnumerable<DemandeCarteRestaurant>> GetDemandeCardByUserId(string UserId);
        Task<IEnumerable<DemandeCarteRestaurant>> GetAllDemandes();
        Task<IEnumerable<DemandeCarteRestaurant>> GetAllPendigDemandes();
        Task RemoveDemandeCard(string partitionkey);
        Task UpdateDemandeCard(string partitionkey, DemandeCarteRestaurant DemandeCarteRestaurant);
    }
}
