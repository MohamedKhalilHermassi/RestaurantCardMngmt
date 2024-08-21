using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.Transaction.Business;
using RM.Transaction.Model;

namespace RM.Transaction.API
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionQueryController : ControllerBase
    {

        #region Fields
        private readonly GetTransactionQuery _getTransactionQuery;
        private readonly GetAllTransactionsQuery _getAllTransactionsQuery;
        private readonly GetTransactionsByCardQuery _getTransactionsByCardQuery;
        #endregion
        #region Constructeur
        public TransactionQueryController(

           GetTransactionQuery getTransactionQuery,
           GetAllTransactionsQuery getAllTransactionsQuery,
           GetTransactionsByCardQuery getTransactionsByCardQuery)
        {

            _getTransactionQuery = getTransactionQuery;
            _getAllTransactionsQuery = getAllTransactionsQuery;
            _getTransactionsByCardQuery = getTransactionsByCardQuery;
        } 
        #endregion
        /// <summary>
        /// Retourner toutes les tranasctions
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner toutes les transactions présentes dans la base de données.
        /// </remarks>  
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Transactions>>> GetAllTransactions()
        {
            var transactions = await _getAllTransactionsQuery.ExecuteAsync();
            return Ok(transactions);
        }
        /// <summary>
        /// Retourner une tranasction
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner une transaction en spécifiant son identifiant.
        /// </remarks>  
        [HttpGet("{partitionKey}")]
        public async Task<ActionResult<Transactions>> GetTransaction(string partitionKey)
        {
            var transaction = await _getTransactionQuery.ExecuteAsync(partitionKey);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
        /// <summary>
        /// Retourner une tranasction
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner la liste des transactions effectuée par une carte restaurant spécifique. 
        /// </remarks>  
        [HttpGet("getTransactionByCardResto/{id}")]
        [Authorize]
        public async Task<ActionResult<Transactions>> GetTransactionByCardResto(string id)
        {
            var transactions = await _getTransactionsByCardQuery.ExecuteAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }
    }
}
