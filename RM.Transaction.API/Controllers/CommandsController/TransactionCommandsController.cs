using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Remote;
using Model;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCommandsController : ControllerBase
    {
        private readonly ICarteRestoService _carteRestoService;


        private readonly AddTransactionCommand _addTransactionCommand;
        private readonly RemoveTransactionCommand _removeTransactionCommand;
        private readonly UpdateTransactionCommand _updateTransactionCommand;
       

        public TransactionCommandsController(
            ICarteRestoService carteRestoService,
            AddTransactionCommand addTransactionCommand,
            RemoveTransactionCommand removeTransactionCommand,
            UpdateTransactionCommand updateTransactionCommand
          )
        {
            _addTransactionCommand = addTransactionCommand;
            _removeTransactionCommand = removeTransactionCommand;
            _updateTransactionCommand = updateTransactionCommand;
            _carteRestoService = carteRestoService;


        }


        [HttpPost("")]
        public async Task<ActionResult<Transactions>> AddTransaction(Transactions transaction)
        {
           
                transaction.Type = false;
                await _addTransactionCommand.ExecuteAsync(transaction);
                return CreatedAtAction(nameof(AddTransaction), new { id = transaction.Id }, transaction);
         
        }

        [HttpPost("addTransactionSimulation")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Transactions>> AddTransactionSimulator(Transactions transaction)
        {
            var check = await _carteRestoService.verifyCarteRestoSolde(transaction.CarteRestoId, transaction.Montant);
            if (check == true)
            {
                transaction.Type = false;
                await _addTransactionCommand.ExecuteAsync(transaction);
                return CreatedAtAction(nameof(AddTransactionSimulator), new { id = transaction.Id }, transaction);
            }
            else
            {
                return NotFound(new { error = "vous n'avez pas assez de solde"});
            }
        }
        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> deleteTransaction(string partitionkey)
        {
            await _removeTransactionCommand.ExecuteAsync(partitionkey);


            return NoContent();
        }



    }
    }
