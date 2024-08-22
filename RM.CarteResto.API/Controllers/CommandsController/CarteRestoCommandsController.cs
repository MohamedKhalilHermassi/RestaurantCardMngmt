using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NotificationsClient;
using RM.CarteResto.Business;
using RM.CarteResto.Model;
using RM.Notif.Business.Commands;
using RM.Transaction.Remote;

namespace RM.CarteResto.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarteRestoCommandsController : ControllerBase
    {
        #region Fields
        private readonly AddCardCommand _addCardCommand;
        private readonly ChargeCardCommand _chargeCardCommand;
        private readonly DischargeCardCommand _dischargeCardCommand;
        private readonly RemoveCardCommand _removeCardCommand;
        private readonly UpdateCardCommand _updateCardCommand;
        private readonly RechargeCardEmail _rechargedCardEmail;
        private readonly ITransactionServiceContract _transactionService;
        #endregion


        #region Constructeur
        public CarteRestoCommandsController(
    AddCardCommand addCardCommand,
    ChargeCardCommand chargeCardCommand,
    DischargeCardCommand dischargeCardCommand,
    RemoveCardCommand removeCardCommand,
    UpdateCardCommand updateCardCommand,
    RechargeCardEmail rechargedCardEmail,
    ITransactionServiceContract transactionService)
        {
            _rechargedCardEmail = rechargedCardEmail;
            _addCardCommand = addCardCommand;
            _chargeCardCommand = chargeCardCommand;
            _dischargeCardCommand = dischargeCardCommand;
            _removeCardCommand = removeCardCommand;
            _updateCardCommand = updateCardCommand;
            _transactionService = transactionService;
        } 
        #endregion

        /// <summary>
        /// Créer une nouvelle carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de créer une nouvelle carte restaurant pour un utilisateur spécifié.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<CarteRestaurant>> AddCard(CarteRestaurant card)
        {
            await _addCardCommand.ExecuteAsync(card);
            return CreatedAtAction(nameof(AddCard), new { id = card.Id }, card);
        }
        /// <summary>
        /// Supprimer une carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de supprimer une carte restaurant en utilisant son identifiant unique (partitionKey).
        /// </remarks>
        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> DeleteCard(string partitionkey)
        {
            await _removeCardCommand.ExecuteAsync(partitionkey);
            return NoContent();
        }
        /// <summary>
        /// Modifier une carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de modifier une carte restaurant en utilisant son identifiant unique (partitionKey).
        /// </remarks>
        [HttpPut("{partitionkey}")]
        public async Task<IActionResult> UpdateCard(string partitionkey, CarteRestaurant card)
        {
            await _updateCardCommand.ExecuteAsync(partitionkey, card);
            return NoContent();
        }
        /// <summary>
        /// Recharger une carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de recharger une carte restaurant. Elle prend 2 paramètres : l'identifiant de la carté à recharger et le montant de la recharge entré par l'administrateur.
        /// </remarks>
        [HttpPut("chargeCard/{partitionkey}/{montant}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ChargeCard(string partitionkey, float montant)
        {
            
            await _chargeCardCommand.ExecuteAsync(partitionkey, montant);
            return NoContent();

        }
        /// <summary>
        /// Décharger une carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de décharger une carte restaurant. Elle prend 3 paramètres : l'identifiant de la carte à recharger ,le montant et le description de la transaction. Cette méthode implique aussi la création d'une transaction relative à cette carte restaruant spécifique ayant les mêmes valeurs {montant, desctiption}.
        /// </remarks>
        [HttpPut("dischargeCard/{partitionkey}/{montant}/{description}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DischargeCard(string partitionkey, float montant,string description)
        {
          
            await _dischargeCardCommand.ExecuteAsync(partitionkey,montant,description);

            return NoContent(); 

        }
    }
    }
