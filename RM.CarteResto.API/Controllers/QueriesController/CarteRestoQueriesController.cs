﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business;
using Model;


namespace API
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class CarteRestoQueriesController : ControllerBase
    {
        GetAllCardsQuery _getAllCardsQuery;
        GetCardByUserIdQuery _getCardByUserIdQuery;
        GetCardQuery _getCardQuery;

        public CarteRestoQueriesController(
            GetAllCardsQuery getAllCardsQuery, 
            GetCardByUserIdQuery getCardByUserIdQuery, 
            GetCardQuery getCardQuery)
        {
            _getAllCardsQuery = getAllCardsQuery;
            _getCardByUserIdQuery = getCardByUserIdQuery;
            _getCardQuery = getCardQuery;
        }
        /// <summary>
        /// Retourner toutes les cartes restaurants
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner toutes les cartes restaurant présentes dans la base de données.
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CarteRestaurant>>> GetAllCards()
        {
            var cards = await _getAllCardsQuery.ExecuteAsync();
            return Ok(cards);
        }
        /* 
                [HttpGet("GetTransactionsFromCardsService/{id}")]
                 public async Task<ActionResult<TransactionByIdReply>> GetAllTransactionsInCardsService(string id)
                  {
                      var client = _grpcConfigTransaction.CreateGrpcClient<ITransactionServiceContract>();


                      Guid myGuid = new Guid(id);
                      var reply = await client.getTransactionById(
                          new TransactionByIdRequest { Id = myGuid });
                      return Ok(reply);

                  }*/

        /// <summary>
        /// Retourner une carte restaurant spécifique
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner une carte restaurant spécifique selon son identifiant.
        /// </remarks>
        [HttpGet("{partitionkey}")]
        public async Task<ActionResult<CarteRestaurant>> GetCardById(string partitionkey)
        {
            var transaction = await _getCardQuery.ExecuteAsync(partitionkey);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
        /// <summary>
        /// Retourner la carte restaurant d'un utilisateur donné
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner la carte restaurant d'un utilisateur spécifique selon son identifiant.
        /// </remarks>
        [HttpGet("getCardByUserId/{id}")]
        [Authorize]
        public async Task<ActionResult<CarteRestaurant>> GetCardByUserId(string id)
        {
            var card = await _getCardByUserIdQuery.ExecuteAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }
    }
}
