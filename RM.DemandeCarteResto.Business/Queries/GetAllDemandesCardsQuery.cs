using Abstraction;
using Model;

namespace Business
{
    public class GetAllDemandesCardsQuery
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        public GetAllDemandesCardsQuery(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        }
        public async Task<IEnumerable<DemandeCarteRestaurant>> ExecuteAsync()
        {
            return await _demandeCarteRepository.GetAllDemandes();
        }
    }
}
