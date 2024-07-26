using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Model.Entity;

namespace RM.DemandeCarteResto.Business.Commands
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
