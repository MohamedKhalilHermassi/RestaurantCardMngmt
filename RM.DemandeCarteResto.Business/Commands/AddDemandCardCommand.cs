using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
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
