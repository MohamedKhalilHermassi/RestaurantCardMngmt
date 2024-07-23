using RM.DemandeCarteResto.Model.Entity;
namespace RM.DemandeCarteResto.Abstraction.Queries
{
    public interface IDemandeCarteRestoQueries
    {
        Task<DemandeCarteRestaurant> getDemandeCardById(string partitionkey);
        Task<IEnumerable<DemandeCarteRestaurant>> getAllDemandesCards();
        Task<IEnumerable<DemandeCarteRestaurant>> getAllPendigDemandes();

    }
}                                                                       
