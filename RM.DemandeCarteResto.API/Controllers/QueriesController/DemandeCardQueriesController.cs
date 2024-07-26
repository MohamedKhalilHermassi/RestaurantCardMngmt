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


        GetAllDemandesCardsQuery _getAllDemandesCardsQuery;
        GetAllPendingDemandsQuery _getAllPendingDemandsQuery;
        GetDemandCardByIdQuery _getDemandCardByIdQuery;
        GetDemandeCardByUserIdQuery _getDemandeCardByUserIdQuery;

        public DemandeCardQueriesController(GetAllDemandesCardsQuery getAllDemandesCardsQuery, GetAllPendingDemandsQuery getAllPendingDemandsQuery, GetDemandCardByIdQuery getDemandCardByIdQuery, GetDemandeCardByUserIdQuery getDemandeCardByUserIdQuery)
        {
            _getAllDemandesCardsQuery = getAllDemandesCardsQuery;
            _getAllPendingDemandsQuery = getAllPendingDemandsQuery;
            _getDemandCardByIdQuery = getDemandCardByIdQuery;
            _getDemandeCardByUserIdQuery = getDemandeCardByUserIdQuery;
        }

        [HttpGet]
       // [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<DemandeCarteRestaurant>>> GetAllDemandesCards()
        {
            var demandesCards = await _getAllDemandesCardsQuery.ExecuteAsync();
            return Ok(demandesCards);
        }
        [HttpGet("pending")]
    //    [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<DemandeCarteRestaurant>>> GetAllPendingDemandesCards()
        {
            var pendingDemandesCards = await _getAllPendingDemandsQuery.ExecuteAsync();
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
            var demandeCard = await _getDemandCardByIdQuery.ExecuteAsync(partitionkey);
            if (demandeCard == null)
            {
                return NotFound();
            }
            return Ok(demandeCard);
        }
        [HttpGet("getDemandeByUserId/{id}")]
      //  [Authorize]
        public async Task<ActionResult<DemandeCarteRestaurant>> GetDemandeByUserId(string id)
        {
            var demandeCard = await _getDemandeCardByUserIdQuery.ExecuteAsync(id);
            if (demandeCard == null)
            {
                return NotFound();
            }
            return Ok(demandeCard);
        }
    }
}
