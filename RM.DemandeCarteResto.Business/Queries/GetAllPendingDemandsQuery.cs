using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
{
    public class GetAllPendingDemandsQuery
    {
        #region Fields
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        #endregion
        #region Constructeur
        public GetAllPendingDemandsQuery(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        } 
        #endregion
        public async Task<IEnumerable<DemandeCarteRestaurant>> ExecuteAsync()
        {
            return await _demandeCarteRepository.GetAllPendigDemandes();
        }
    }
}
