using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.DemandeCarteResto.Business.Queries;
using RM.DemandeCarteResto.Model.Entity;

namespace RM.DemandeCarteResto.API.Controllers.QueriesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeCardQueriesController : ControllerBase
    { 
        private readonly DemandeCarteRestoQueries _demandeCarteQuery;

        public DemandeCardQueriesController(DemandeCarteRestoQueries demandeCarteQuery)
        {
            _demandeCarteQuery = demandeCarteQuery ?? throw new ArgumentNullException(nameof(_demandeCarteQuery));
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<DemandeCarteRestaurant>>> GetAllDemandesCards()
        {
            var demandesCards = await _demandeCarteQuery.getAllDemandesCards();
            return Ok(demandesCards);
        }
        [HttpGet("pending")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<DemandeCarteRestaurant>>> GetAllPendingDemandesCards()
        {
            var pendingDemandesCards = await _demandeCarteQuery.getAllPendigDemandes();
            return Ok(pendingDemandesCards);
        }
        /* [HttpGet("GetTransactionsFromCardsService")]
         public async Task<ActionResult<TransactionByIdReply>> GetAllTransactionsInCardsService()
         {
             using var channel = GrpcChannel.ForAddress("https://localhost:7105");
             var client = channel.CreateGrpcService<ITransactionServiceContract>();
             string guidString = "f8a5fe05-8bb6-4e4f-7aa0-08dc9b6aa388";
             Guid myGuid = new Guid(guidString);
             var reply = await client.getTransactionById(
                 new TransactionByIdRequest { Id = myGuid });
             return Ok(reply);

         }*/

        [HttpGet("{partitionkey}")]
        public async Task<ActionResult<DemandeCarteRestaurant>> GetDemandeCardById(string partitionkey)
        {
            var demandeCard = await _demandeCarteQuery.getDemandeCardById(partitionkey);
            if (demandeCard == null)
            {
                return NotFound();
            }
            return Ok(demandeCard);
        }
        [HttpGet("getDemandeByUserId/{id}")]
        [Authorize]
        public async Task<ActionResult<DemandeCarteRestaurant>> GetDemandeByUserId(string id)
        {
            var demandeCard = await _demandeCarteQuery.getDemandeCardByUserId(id);
            if (demandeCard == null)
            {
                return NotFound();
            }
            return Ok(demandeCard);
        }
    }
}
