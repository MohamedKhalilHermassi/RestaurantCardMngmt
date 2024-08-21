using RM.CarteResto.Remote;
using RM.DemandeCarteResto.Abstraction;

namespace RM.DemandeCarteResto.Business
{
    public class AcceptDemandCardCommand
    {
        #region Fields
        private readonly IDemandeCarteRestoRepository _demandeCardRepository;
        private readonly ICarteRestoService _carteRestoService;
        #endregion

        #region Constructeur
        public AcceptDemandCardCommand(IDemandeCarteRestoRepository demandeCardRepository, ICarteRestoService carteRestoService
)
        {
            _carteRestoService = carteRestoService;
            _demandeCardRepository = demandeCardRepository;
        } 
        #endregion
        public async Task ExecuteAsync(string partitionkey)
        {
            var demand = await _demandeCardRepository.GetDemandeCardById(partitionkey);
            if (demand == null)
            {
                throw new KeyNotFoundException($"DemandeCard with ID {partitionkey} not found");
            }
            demand.Status = true;
            await _demandeCardRepository.UpdateDemandeCard(demand.PartitionKey, demand);


            var foundDemand = await _demandeCardRepository.GetDemandeCardById(partitionkey);
            Random ran = new Random();

            String b = "0123456789";

            int length = 16;

            String random = "";

            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(10);
                random = random + b.ElementAt(a);
            }
            var cardResto = new CarteRestoByIdReply
            {
                Numero = random,
                Solde = 0,
                TransactionIds = [],
                UserId = foundDemand.UserId,
                UserEmail = foundDemand.UserEmail
            };
            await _carteRestoService.AddCarteResto(cardResto);
        }
    }
}
