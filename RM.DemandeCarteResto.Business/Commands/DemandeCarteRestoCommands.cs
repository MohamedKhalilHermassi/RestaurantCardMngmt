using RM.DemandeCarteResto.Abstraction.Commands;
using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Model.Entity;

namespace RM.DemandeCarteResto.Business.Commands
{
    public class DemandeCarteRestoCommands : IDemandeCarteRestoCommands
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRepo;

        public DemandeCarteRestoCommands(IDemandeCarteRestoRepository demandecarteRepo)
        {
            _demandeCarteRepo = demandecarteRepo;
        }

        public async Task acceptDemandeCard(string partitionkey)
        {
            await _demandeCarteRepo.acceptDemandeCard(partitionkey);
        }

        public async Task<DemandeCarteRestaurant> addDemandeCard(DemandeCarteRestaurant DemandeCarteResto)
        {
            return await _demandeCarteRepo.addDemandeCarte(DemandeCarteResto);
        }

        public async Task rejectDemandeCard(string partitionkey)
        {
            await _demandeCarteRepo.rejectDemandeCard(partitionkey);
        }

        public async Task removeDemandeCard(string partitionkey)
        {
             await _demandeCarteRepo.removeDemandeCard(partitionkey);
        }

        public async Task updateDemandeCard(string partitionkey, DemandeCarteRestaurant DemandeCarteRestaurant)
        {
            await _demandeCarteRepo.updateDemandeCard(partitionkey, DemandeCarteRestaurant);
        }
    }
}
