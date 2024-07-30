using RM.CarteResto.Abstraction;

namespace RM.CarteResto.Business
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
