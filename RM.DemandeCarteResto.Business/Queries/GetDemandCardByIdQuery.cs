using Abstraction;
using Model;

namespace Business
{
    public class GetDemandCardByIdQuery
    {
        private readonly IDemandeCarteRestoRepository _demandeCardRepository;

        public GetDemandCardByIdQuery(IDemandeCarteRestoRepository demandeCardRepository)
        {
            _demandeCardRepository = demandeCardRepository;
        }
        public async Task<DemandeCarteRestaurant> ExecuteAsync(string partiionKey)
        {
            return await _demandeCardRepository.GetDemandeCardById(partiionKey);
        }
    }
}
