using RM.DemandeCarteResto.Abstraction.Queries;
using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Model.Entity;

namespace RM.DemandeCarteResto.Business.Queries
{
    public class DemandeCarteRestoQueries : IDemandeCarteRestoQueries
    {

        private readonly IDemandeCarteRestoRepository _demandeCarteRepo;

        public DemandeCarteRestoQueries(IDemandeCarteRestoRepository demandecarteRepo)
        {
            _demandeCarteRepo = demandecarteRepo;
        }
        public async Task<IEnumerable<DemandeCarteRestaurant>> getAllDemandesCards()
        {
            return await _demandeCarteRepo.getAllDemandes();
        }

        public async Task<IEnumerable<DemandeCarteRestaurant>> getAllPendigDemandes()
        {
            return await _demandeCarteRepo.getAllPendigDemandes();
        }

        public async Task<DemandeCarteRestaurant> getDemandeCardById(string partitionkey)
        {
            return await _demandeCarteRepo.getDemandeCardById(partitionkey);
        }
        public async Task<IEnumerable<DemandeCarteRestaurant>> getDemandeCardByUserId(string DemandeCarteId)
        {
            return await _demandeCarteRepo.getDemandeCardByUserId(DemandeCarteId);
        }
    }
}
