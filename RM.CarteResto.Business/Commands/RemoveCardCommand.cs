using RM.CarteResto.Abstraction.Repositories;

namespace RM.CarteResto.Business.Commands
{
    public class RemoveCardCommand
    {
        private readonly ICarteRestoRepository _carteRestoRepository;

        public RemoveCardCommand(ICarteRestoRepository carteRestoRepository)
        {
            _carteRestoRepository = carteRestoRepository;
        }
        public async Task ExecuteAsync(string partitionKey)
        {
            await _carteRestoRepository.RemoveCard(partitionKey);
        }
    }
}
