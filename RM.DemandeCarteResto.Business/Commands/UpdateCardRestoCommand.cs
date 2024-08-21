using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
{
    public class UpdateCardRestoCommand
    {
        #region Fields
        private readonly IDemandeCarteRestoRepository _demandeCarteRestoRepository;

        #endregion
        #region Constructeur
        public UpdateCardRestoCommand(IDemandeCarteRestoRepository demandeCarteRestoRepository)
        {
            _demandeCarteRestoRepository = demandeCarteRestoRepository;
        } 
        #endregion
        public async Task ExecuteAsync(string partitionKey, DemandeCarteRestaurant demand)
        {
            await _demandeCarteRestoRepository.UpdateDemandeCard(partitionKey, demand);
        }

    }
}
