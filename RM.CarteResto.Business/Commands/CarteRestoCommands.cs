using RM.CarteResto.Abstraction.Commands;
using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Model.Entitiy;


namespace RM.CarteResto.Business.Commands
{
    public class CarteRestoCommands : ICarteRestoCommand
    {
        private readonly ICarteRestoRepository _carteRepo;

        public CarteRestoCommands(ICarteRestoRepository carteRepo)
        {
            _carteRepo = carteRepo;
        }

        public async Task<CarteRestaurant> addCard(CarteRestaurant carteResto)
        {

          return  await _carteRepo.addCard(carteResto);
        }

        public async Task ChargeCard(string partitionkey, float montant, string IdTransaction)
        {
                  await _carteRepo.ChargeCard(partitionkey, montant, IdTransaction);
        }

        public async Task DischargeCard(string partitionkey, float montant, string IdTransaction)
        {
            await _carteRepo.DischargeCard(partitionkey, montant, IdTransaction);
        }

        public async Task removeCard(string partitionkey)
        {
           await _carteRepo.removeCard(partitionkey);
        }

        public async Task updateCard(string partitionkey, CarteRestaurant card)
        {
            await _carteRepo.updateCard(partitionkey, card);
        }
    }
}
