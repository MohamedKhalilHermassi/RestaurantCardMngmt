using RM.DemandeCarteResto.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.DemandeCarteResto.Business.Commands
{
    public class RemoveDemandCardCommand
    {
        private readonly IDemandeCarteRestoRepository _demandeCarteRepository;

        public RemoveDemandCardCommand(IDemandeCarteRestoRepository demandeCarteRepository)
        {
            _demandeCarteRepository = demandeCarteRepository;
        }

        public async Task ExecuteAsync(string partitionKey)
        {
            await _demandeCarteRepository.RemoveDemandeCard(partitionKey);
        }
    }
}               
