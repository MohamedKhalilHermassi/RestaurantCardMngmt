using Microsoft.AspNetCore.Mvc;
using RM.CarteResto.Remote;
using RM.Transaction.Business;
using RM.Transaction.Model;

namespace RM.Transaction.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCommandsController : ControllerBase
    {
        #region Fields
        private readonly ICarteRestoService _carteRestoService;
        private readonly AddTransactionCommand _addTransactionCommand;
        private readonly RemoveTransactionCommand _removeTransactionCommand;
        private readonly UpdateTransactionCommand _updateTransactionCommand;
        #endregion

        #region Contructeur
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
        #endregion

        /// <summary>
        /// Céer une nouvelle transaction
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de créer une nouvelle transaction.
        /// </remarks>  
        [HttpPost("")]
        public async Task<ActionResult<Transactions>> AddTransaction(Transactions transaction)
        {
           
                transaction.Type = false;
                await _addTransactionCommand.ExecuteAsync(transaction);
                return CreatedAtAction(nameof(AddTransaction), new { id = transaction.Id }, transaction);
         
        }
        /// <summary>
        /// Un simulateur de transaction.
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de créer des transactions qui décrémente le solde d'une carte restaurant afin de simuler les transactions à effectuer par la carte restaurant..
        /// </remarks>  
        [HttpPost("addTransactionSimulation")]
       // [Authorize(Roles = "User")]
        public async Task<ActionResult<Transactions>> AddTransactionSimulator(Transactions transaction)
        {
            var check = await _carteRestoService.VerifyCarteRestoSolde(transaction.CarteRestoId, transaction.Montant);
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
        /// <summary>
        /// Supprimer une tranasction
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de supprimer une transaction en spécifiant son identifiant.
        /// </remarks>  
        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> DeleteTransaction(string partitionkey)
        {
            await _removeTransactionCommand.ExecuteAsync(partitionkey);


            return NoContent();
        }
    }
}
