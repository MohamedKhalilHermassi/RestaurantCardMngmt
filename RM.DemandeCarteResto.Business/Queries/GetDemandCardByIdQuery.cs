using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
{
    public class GetDemandCardByIdQuery
    {
        #region Fields
        private readonly IDemandeCarteRestoRepository _demandeCardRepository;

        #endregion
        #region Contructeur
        public GetDemandCardByIdQuery(IDemandeCarteRestoRepository demandeCardRepository)
        {
            _demandeCardRepository = demandeCardRepository;
        } 
        #endregion
        public async Task<DemandeCarteRestaurant> ExecuteAsync(string partiionKey)
        {
            return await _demandeCardRepository.GetDemandeCardById(partiionKey);
        }
    }
}
