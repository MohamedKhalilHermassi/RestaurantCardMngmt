using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
{
    public class UpdateCardRestoCommand
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRestoRepository;

        public UpdateCardRestoCommand(IDemandeCarteRestoRepository demandeCarteRestoRepository)
        {
            _demandeCarteRestoRepository = demandeCarteRestoRepository;
        }
        public async Task ExecuteAsync(string partitionKey, DemandeCarteRestaurant demand)
        {
            await _demandeCarteRestoRepository.UpdateDemandeCard(partitionKey, demand);
        }

    }
}
