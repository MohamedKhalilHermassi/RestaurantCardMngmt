using RM.DemandeCarteResto.Abstraction;

namespace RM.DemandeCarteResto.Business
{
    public class RemoveDemandCardCommand
    {
        #region Fields
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        #endregion
        #region Constructeur
        public RemoveDemandCardCommand(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string partitionKey)
        {
            await _demandeCarteRepository.RemoveDemandeCard(partitionKey);
        }
    }
}               
