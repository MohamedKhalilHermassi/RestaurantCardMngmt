using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.CarteResto.Business.Queries;
using RM.CarteResto.Model.Entitiy;


namespace RM.CarteResto.API.Controllers.QueriesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarteRestoQueriesController : ControllerBase
    {
        private readonly CarteRestoQuery _carteQuery;
        public CarteRestoQueriesController(CarteRestoQuery carteQuery)
        {
            _carteQuery = carteQuery ?? throw new ArgumentNullException(nameof(_carteQuery));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CarteRestaurant>>> GetAllCards()
        {
            var cards = await _carteQuery.getAllCards();
            return Ok(cards);
        }
        [HttpGet("GetTransactionsFromCardsService/{id}")]
        /*  public async Task<ActionResult<TransactionByIdReply>> GetAllTransactionsInCardsService(string id)
          {
              var client = _grpcConfigTransaction.CreateGrpcClient<ITransactionServiceContract>();


              Guid myGuid = new Guid(id);
              var reply = await client.getTransactionById(
                  new TransactionByIdRequest { Id = myGuid });
              return Ok(reply);

          }*/

        [HttpGet("{partitionkey}")]
        public async Task<ActionResult<CarteRestaurant>> GetCardById(string partitionkey)
        {
            var transaction = await _carteQuery.getCardById(partitionkey);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
        [HttpGet("getCardByUserId/{id}")]
        [Authorize]
        public async Task<ActionResult<CarteRestaurant>> GetCardByUserId(string id)
        {
            var card = await _carteQuery.getCardByUserId(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }
    }
}
