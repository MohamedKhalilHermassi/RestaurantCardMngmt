using Microsoft.EntityFrameworkCore;
using RM.CarteResto.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.CarteResto.Business.Commands
{
    public class DecrementBalanceCommand
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public DecrementBalanceCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task ExecuteAsync(string partitionkey, float montant)

        {
            var card = await _carteRestoRepository.GetCard(partitionkey);
            card.Solde -= montant;
            await _carteRestoRepository.UpdateCard(partitionkey, card);

        }

    }
}
