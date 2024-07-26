using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Model.Entity;


namespace RM.DemandeCarteResto.Business.Commands
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
