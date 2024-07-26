using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.DemandeCarteResto.Business.Queries
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
