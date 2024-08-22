using RM.CarteResto.Abstraction;

namespace RM.CarteResto.Business
{
    public class DecrementBalanceCommand
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;

        #endregion

        #region Constructeur
        public DecrementBalanceCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        } 
        #endregion
        public async Task ExecuteAsync(string partitionkey, float montant)

        {
            var card = await _carteRestoRepository.GetCard(partitionkey);
            card.Solde -= montant;
            await _carteRestoRepository.UpdateCard(partitionkey, card);

        }

    }
}
