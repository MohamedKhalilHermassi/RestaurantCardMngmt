using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Model.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.CarteResto.Business.Commands
{
    public class AddCardCommand
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public AddCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task ExecuteAsync(CarteRestaurant carte)
        {
            await _carteRestoRepository.AddCard(carte);
        }
    }
}
