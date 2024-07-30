using RM.DemandeCarteResto.Abstraction;

namespace RM.DemandeCarteResto.Business
{
    public class RemoveDemandCardCommand
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        public RemoveDemandCardCommand(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        }

        public async Task ExecuteAsync(string partitionKey)
        {
            await _demandeCarteRepository.RemoveDemandeCard(partitionKey);
        }
    }
}               
