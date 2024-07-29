using Abstraction;
using Model;

namespace Business
{
    public class GetDemandeCardByUserIdQuery
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRestoRepository;

        public GetDemandeCardByUserIdQuery(IDemandeCarteRestoRepository demandeCarteRestoRepository)
        {
            _demandeCarteRestoRepository = demandeCarteRestoRepository;
        }

        public async Task<IEnumerable<DemandeCarteRestaurant>> ExecuteAsync(string userPartitionKey)
        {
            return await _demandeCarteRestoRepository.GetDemandeCardByUserId(userPartitionKey);
        }
    }
}
