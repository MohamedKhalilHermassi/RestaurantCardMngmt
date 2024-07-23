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
        private readonly CarteRestoCommands _carteCommands;
        private readonly ITransactionServiceContract _transactionService;
        public CarteRestoCommandsController(CarteRestoCommands carteCommands, ITransactionServiceContract transactionService)
        {
            _carteCommands = carteCommands;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<CarteRestaurant>> addCard(CarteRestaurant card)
        {
            await _carteCommands.addCard(card);
            return CreatedAtAction(nameof(addCard), new { id = card.Id }, card);
        }

        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> deleteCard(string partitionkey)
        {
            await _carteCommands.removeCard(partitionkey);
            return NoContent();
        }

        [HttpPut("{partitionkey}")]
        public async Task<IActionResult> updateCard(string partitionkey, CarteRestaurant card)
        {
            await _carteCommands.updateCard(partitionkey, card);
            return NoContent();
        }

        [HttpPut("chargeCard/{partitionkey}/{montant}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> chargeCard(string partitionkey, float montant)
        {
            var trasnaction = new TransactionByIdReply
            {
                CarteRestoId = partitionkey,
                Description = "Recharge de la carte restaurant ",
                Montant = montant,
                Type = true
            };
            await _transactionService.addTransaction(trasnaction);
            Console.WriteLine("Hello World!");

            Console.WriteLine(partitionkey);
            var IdTran = await _transactionService.getTransactionByCardId(partitionkey);
            await _carteCommands.ChargeCard(partitionkey, montant, IdTran.PartitionKey);

            return NoContent();

        }
        [HttpPut("dischargeCard/{partitionkey}/{montant}/{description}")]
       // [Authorize(Roles = "User")]
        public async Task<IActionResult> DischargeCard(string partitionkey, float montant,string description)
        {
            var trasnaction = new TransactionByIdReply
            {
                CarteRestoId = partitionkey,
                Description = description,
                Montant = montant,
                Type = false
            };
            await _transactionService.addTransaction(trasnaction);
            var IdTran = await _transactionService.getTransactionByCardId(partitionkey);
            await _carteCommands.DischargeCard(partitionkey, montant, IdTran.Id.ToString());

            return NoContent();

        }
    }
    }
