using RM.DemandeCarteResto.Abstraction;

namespace RM.DemandeCarteResto.Business
{
    public class RejectDemandCardCommand
    {
        #region field
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;
        #endregion

        #region Constructeur
        public RejectDemandCardCommand(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        } 
        #endregion

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
