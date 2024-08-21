using RM.DemandeCarteResto.Abstraction;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.Business
{
    public class GetDemandeCardByUserIdQuery
    {
        #region Fields
        private readonly IDemandeCarteRestoRepository _demandeCarteRestoRepository;

        #endregion
        #region Constructeur
        public GetDemandeCardByUserIdQuery(IDemandeCarteRestoRepository demandeCarteRestoRepository)
        {
            _demandeCarteRestoRepository = demandeCarteRestoRepository;
        } 
        #endregion

        public async Task<IEnumerable<DemandeCarteRestaurant>> ExecuteAsync(string userPartitionKey)
        {
            return await _demandeCarteRestoRepository.GetDemandeCardByUserId(userPartitionKey);
        }
    }
}
