using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionQueryController : ControllerBase
    {

        private readonly GetTransactionQuery _getTransactionQuery;
        private readonly GetAllTransactionsQuery _getAllTransactionsQuery;
        private readonly GetTransactionsByCardQuery _getTransactionsByCardQuery;

        public TransactionQueryController(
           
            GetTransactionQuery getTransactionQuery,
            GetAllTransactionsQuery getAllTransactionsQuery,
            GetTransactionsByCardQuery getTransactionsByCardQuery)
        {

            _getTransactionQuery = getTransactionQuery;
            _getAllTransactionsQuery = getAllTransactionsQuery;
            _getTransactionsByCardQuery = getTransactionsByCardQuery;
        }
        
        [HttpGet]
      //  [Authorize]
        public async Task<ActionResult<List<Transactions>>> GetAllTransactions()
        {
            var transactions = await _getAllTransactionsQuery.ExecuteAsync();
            return Ok(transactions);
        }
        
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
