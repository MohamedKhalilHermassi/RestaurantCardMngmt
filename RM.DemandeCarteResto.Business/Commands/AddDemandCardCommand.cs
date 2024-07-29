using Abstraction;
using Model;

namespace Business
{
    public class AddDemandCardCommand
    {
        private readonly IDemandeCarteRestoRepository _demandCardRepository;

        public AddDemandCardCommand(IDemandeCarteRestoRepository demandCardRepository)
        {
            _demandCardRepository = demandCardRepository;
        }

        public async Task<DemandeCarteRestaurant> ExecuteAsync(DemandeCarteRestaurant demande)
        {
           return await _demandCardRepository.AddDemandeCarte(demande);
        }
    }
}
