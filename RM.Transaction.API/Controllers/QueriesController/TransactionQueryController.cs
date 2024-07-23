using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.Transaction.Business.Queries;
using RM.Transaction.Model.Entity;

namespace RM.Transaction.API.Controllers.QueriesController
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionQueryController : ControllerBase
    {
        private readonly TransactionQuery _transactionQuery;

        public TransactionQueryController(TransactionQuery transactionQuery)
        {
            _transactionQuery = transactionQuery ?? throw new ArgumentNullException(nameof(_transactionQuery));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Transactions>>> GetAllTransactions()
        {
            var transactions = await _transactionQuery.getAllTransactions();
            return Ok(transactions);
        }

        [HttpGet("{partitionKey}")]
        public async Task<ActionResult<Transactions>> GetTransactionById(string partitionKey)
        {
            var transaction = await _transactionQuery.getTransactionById(partitionKey);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpGet("getTransactionByCardRestoId/{id}")]
        [Authorize]
        public async Task<ActionResult<Transactions>> GetTransactionByCardRestoId(string id)
        {
            var transactions = await _transactionQuery.getTransactionsByCardRestoId(id);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }
    }
}
