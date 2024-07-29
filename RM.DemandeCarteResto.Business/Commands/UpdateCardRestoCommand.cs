using Abstraction;
using Model;

namespace Business
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
