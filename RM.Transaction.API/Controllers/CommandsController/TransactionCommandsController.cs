using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.CarteResto.Remote.Contracts;
using RM.Transaction.Business.Commands;
using RM.Transaction.Model.Entity;

namespace RM.Transaction.API.Controllers.CommandsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCommandsController : ControllerBase
    {
        private readonly ICarteRestoService _carteRestoService;

        private readonly TransactionCommand _transactionCommands;

        public TransactionCommandsController(TransactionCommand transactionCommands, ICarteRestoService carteRestoService)
        {
            _transactionCommands = transactionCommands ?? throw new ArgumentNullException(nameof(_transactionCommands));
            _carteRestoService = carteRestoService;
        }

        [HttpPost("")]
        public async Task<ActionResult<Transactions>> addTransaction(Transactions transaction)
        {
           
                transaction.Type = false;
                await _transactionCommands.addTransaction(transaction);
                return CreatedAtAction(nameof(addTransaction), new { id = transaction.Id }, transaction);
         
        }

        [HttpPost("addTransactionSimulation")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Transactions>> addTransactionSimulator(Transactions transaction)
        {
            var check = await _carteRestoService.verifyCarteRestoSolde(transaction.CarteRestoId, transaction.Montant);
            if (check == true)
            {
                transaction.Type = false;
                await _transactionCommands.addTransaction(transaction);
                return CreatedAtAction(nameof(addTransactionSimulator), new { id = transaction.Id }, transaction);
            }
            else
            {
                return NotFound(new { error = "vous n'avez pas assez de solde" });
            }
        }

        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> deleteTransaction(string partitionkey)
        {
            await _transactionCommands.removeTransaction(partitionkey);


            return NoContent();
        }

        [HttpPut("{partitionkey}")]
        public async Task<IActionResult> updateTransaction(string partitionkey, Transactions transaction)
        {
            await _transactionCommands.updateTransaction(partitionkey, transaction);

            return NoContent();

        }
    }
}
