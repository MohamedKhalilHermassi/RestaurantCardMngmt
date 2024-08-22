using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.DemandeCarteResto.Business;
using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeCardQueriesController : ControllerBase
    {


        #region Fields
        GetAllDemandesCardsQuery _getAllDemandesCardsQuery;
        GetAllPendingDemandsQuery _getAllPendingDemandsQuery;
        GetDemandCardByIdQuery _getDemandCardByIdQuery;
        GetDemandeCardByUserIdQuery _getDemandeCardByUserIdQuery;
        #endregion

        #region Constructeur
        public DemandeCardQueriesController(GetAllDemandesCardsQuery getAllDemandesCardsQuery, GetAllPendingDemandsQuery getAllPendingDemandsQuery, GetDemandCardByIdQuery getDemandCardByIdQuery, GetDemandeCardByUserIdQuery getDemandeCardByUserIdQuery)
        {
            _getAllDemandesCardsQuery = getAllDemandesCardsQuery;
            _getAllPendingDemandsQuery = getAllPendingDemandsQuery;
            _getDemandCardByIdQuery = getDemandCardByIdQuery;
            _getDemandeCardByUserIdQuery = getDemandeCardByUserIdQuery;
        } 
        #endregion

        /// <summary>
        /// Retourner toutes les demandes de carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de supprimer une demande de carte restaurant en spécifiant son identifiant.
        /// </remarks>   
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<DemandeCarteRestaurant>>> GetAllDemandesCards()
        {
            var demandesCards = await _getAllDemandesCardsQuery.ExecuteAsync();
            return Ok(demandesCards);
        }
        /// <summary>
        /// Retourner les demandes de carte restaurant ayant un statut en cours.
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner seulement les demandes de carte restaurant qui sont en cours de traitement.
        /// </remarks>
        [HttpGet("pending")]
        [Authorize(Roles ="Admin")]
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
        /// <summary>
        /// Retourner une demande de carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner une demande de carte restaurant bien détérminée.
        /// </remarks>   
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
        /// <summary>
        /// Retourner la liste de demandes de carte restaurant 
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner la liste de demandes de carte retaurant relative à un utilisateur spécifique.
        /// </remarks>   
        [HttpGet("getDemandeByUserId/{id}")]
        [Authorize]
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
