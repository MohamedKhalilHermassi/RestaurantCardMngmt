using RM.DemandeCarteResto.Abstraction.Repositories;


namespace RM.DemandeCarteResto.Business.Commands
{
    public class RejectDemandCardCommand
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        public RejectDemandCardCommand(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        }

        public async Task ExecuteAsync(string partitionkey)
        {
            var card = await _demandeCarteRepository.GetDemandeCardById(partitionkey);  
            if (card == null)
            {
                throw new KeyNotFoundException($"DemandeCard with ID {partitionkey} not found");
            }
            card.Status = false;

            await _demandeCarteRepository.UpdateDemandeCard(card.PartitionKey, card);
        }
    }
}
