using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
{
    public class AddDemandCardCommand
    {
        #region Fields
        private readonly IDemandeCarteRestoRepository _demandCardRepository;

        #endregion
        #region Constructeur
        public AddDemandCardCommand(IDemandeCarteRestoRepository demandCardRepository)
        {
            _demandCardRepository = demandCardRepository;
        } 
        #endregion

        public async Task<DemandeCarteRestaurant> ExecuteAsync(DemandeCarteRestaurant demande)
        {
           return await _demandCardRepository.AddDemandeCarte(demande);
        }
    }
}
