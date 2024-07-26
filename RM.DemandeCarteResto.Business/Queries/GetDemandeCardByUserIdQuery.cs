using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.DemandeCarteResto.Business.Queries
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
