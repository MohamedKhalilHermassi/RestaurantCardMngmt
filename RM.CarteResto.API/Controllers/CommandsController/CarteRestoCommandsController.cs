using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.CarteResto.Business.Commands;
using RM.CarteResto.Model.Entitiy;
using RM.Transaction.Remote.Contracts;

namespace RM.CarteResto.API.Controllers.CommandsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarteRestoCommandsController : ControllerBase
    {
        private readonly AddCardCommand _addCardCommand;
        private readonly ChargeCardCommand _chargeCardCommand;
        private readonly DischargeCardCommand _dischargeCardCommand;
        private readonly RemoveCardCommand _removeCardCommand;
        private readonly UpdateCardCommand _updateCardCommand;

        private readonly ITransactionServiceContract _transactionService;

        public CarteRestoCommandsController(
            AddCardCommand addCardCommand,
            ChargeCardCommand chargeCardCommand,
            DischargeCardCommand dischargeCardCommand, 
            RemoveCardCommand removeCardCommand, 
            UpdateCardCommand updateCardCommand, 
            ITransactionServiceContract transactionService)
        {
            _addCardCommand = addCardCommand;
            _chargeCardCommand = chargeCardCommand;
            _dischargeCardCommand = dischargeCardCommand;
            _removeCardCommand = removeCardCommand;
            _updateCardCommand = updateCardCommand;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<CarteRestaurant>> addCard(CarteRestaurant card)
        {
            await _addCardCommand.ExecuteAsync(card);
            return CreatedAtAction(nameof(addCard), new { id = card.Id }, card);
        }

        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> deleteCard(string partitionkey)
        {
            await _removeCardCommand.ExecuteAsync(partitionkey);
            return NoContent();
        }

        [HttpPut("{partitionkey}")]
        public async Task<IActionResult> updateCard(string partitionkey, CarteRestaurant card)
        {
            await _updateCardCommand.ExecuteAsync(partitionkey, card);
            return NoContent();
        }

        [HttpPut("chargeCard/{partitionkey}/{montant}")]
    //    [Authorize(Roles ="Admin")]
        public async Task<IActionResult> chargeCard(string partitionkey, float montant)
        {
            
            await _chargeCardCommand.ExecuteAsync(partitionkey, montant);    

            return NoContent();

        }

        [HttpPut("dischargeCard/{partitionkey}/{montant}/{description}")]
       // [Authorize(Roles = "User")]
        public async Task<IActionResult> DischargeCard(string partitionkey, float montant,string description)
        {
          
            await _dischargeCardCommand.ExecuteAsync(partitionkey,montant,description);

            return NoContent();

        }
    }
    }
