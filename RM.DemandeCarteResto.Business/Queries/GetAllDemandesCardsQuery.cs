using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.DemandeCarteResto.Business.Queries
{
    public class GetAllDemandesCardsQuery
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        public GetAllDemandesCardsQuery(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        }
        public async Task<IEnumerable<DemandeCarteRestaurant>> ExecuteAsync()
        {
            return await _demandeCarteRepository.GetAllDemandes();
        }
    }
}
