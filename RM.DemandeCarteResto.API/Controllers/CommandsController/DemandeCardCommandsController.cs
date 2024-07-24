using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using RM.CarteResto.Remote.Contracts;
using RM.DemandeCarteResto.Business.Commands;
using RM.DemandeCarteResto.Business.Queries;
using RM.DemandeCarteResto.Model.Entity;
using RM.Notif.Business.Commands;
using RM.Notif.Business.Queries;
using RM.Notif.Model.Entities;

namespace RM.DemandeCarteResto.API.Controllers.CommandsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeCardCommandsController : ControllerBase
    {
        private readonly ICarteRestoService _carteRestoService;
        private readonly NotificationCommands notificationCommands;
        private readonly NotificationQueries notifcationQueries; 
        private readonly HubConnection _hubConnection;
        private readonly DemandeCarteRestoCommands _demandeCartResto;
        private readonly DemandeCarteRestoQueries _demandeCartRestoQueries;
        public DemandeCardCommandsController(NotificationCommands _notificationCommands,DemandeCarteRestoCommands demandeCarteCammand, ICarteRestoService carteRestoService, DemandeCarteRestoQueries demandeCartRestoQueries)
        {
            var hubUrl = "https://localhost:7279/notificationHub";
            notificationCommands = _notificationCommands;
            _carteRestoService = carteRestoService;
            _demandeCartResto = demandeCarteCammand;
            _demandeCartRestoQueries = demandeCartRestoQueries;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

        }

        [HttpPost]
        public async Task<ActionResult<DemandeCarteRestaurant>> addDemandeCarteResto(DemandeCarteRestaurant demandeCard)
        {
            var id = Guid.NewGuid();
           var res =  await _demandeCartResto.addDemandeCard(demandeCard);
            if (res==null)
                return Ok(res);
            else
            {
                var newNotif = new Notification
                {
                    
                    NotificationId = id,
                    PartitionKey = id.ToString(),
                    Message = "Une nouvelle demande de carte restaurant a été déposée.",
                    ReceiverId = "admin@admin.com"
                };
                await notificationCommands.addNotification(newNotif);

                await _hubConnection.StartAsync(); 
                await _hubConnection.InvokeAsync("ReceiveMessage");


                return CreatedAtAction(nameof(addDemandeCarteResto), new { id = demandeCard.Id }, demandeCard);

            }
        }
                
        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> deleteDemandeCard(string partitionkey)
        {
            await _demandeCartResto.removeDemandeCard(partitionkey);
            return NoContent();
        }

        [HttpPut("{partitionkey}")]
        public async Task<IActionResult> updateDemandeCard(string partitionkey, DemandeCarteRestaurant demandeCard)
        {
            await _demandeCartResto.updateDemandeCard(partitionkey, demandeCard);
            return NoContent();

        }
        [HttpPut("approve/{partitionkey}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> approveDemandeCard(string partitionkey)
        {
            await _demandeCartResto.acceptDemandeCard(partitionkey);
            var foundDemand = await _demandeCartRestoQueries.getDemandeCardById(partitionkey);
            Console.WriteLine(foundDemand.ToString());
            Random ran = new Random();

            String b = "0123456789";

            int length = 16;

            String random = "";

            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(10);
                random = random + b.ElementAt(a);
            }
            var cardResto = new CarteRestoByIdReply
            {
                Numero = random,
                Solde = 0,
                TransactionIds = [],
                UserId = foundDemand.UserId,
                UserEmail = foundDemand.UserEmail
            };
            await _carteRestoService.addCarteResto(cardResto);
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync("NotifyUserAccept",foundDemand.UserEmail);
           // await _emailService.SendSimpleMessageAsync();
            return NoContent();

        }
        [HttpPut("reject/{partitionkey}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> rejectDemandeCard(string partitionkey)
        {
            var foundDemand = await _demandeCartRestoQueries.getDemandeCardById(partitionkey);

            await _demandeCartResto.rejectDemandeCard(partitionkey);
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync("NotifyUserReject",foundDemand.UserEmail);
         //   await _emailService.SendSimpleMessageAsyncReject();
            return NoContent();
        }
    }
}
