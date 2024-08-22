using RM.CarteResto.Abstraction;

namespace RM.CarteResto.Business
{
    public class RemoveCardCommand
    {
        #region Fields
        private readonly ICarteRestoRepository _carteRestoRepository;

        #endregion
        #region Constructeur
        public RemoveCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        } 
        #endregion
        public async Task ExecuteAsync(string partitionKey)
        {
            await _carteRestoRepository.RemoveCard(partitionKey);
        }
    }
}
