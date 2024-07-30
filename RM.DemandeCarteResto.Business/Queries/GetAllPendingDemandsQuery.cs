using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
{
    public class GetAllPendingDemandsQuery
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        public GetAllPendingDemandsQuery(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        }
        public async Task<IEnumerable<DemandeCarteRestaurant>> ExecuteAsync()
        {
            return await _demandeCarteRepository.GetAllPendigDemandes();
        }
    }
}
