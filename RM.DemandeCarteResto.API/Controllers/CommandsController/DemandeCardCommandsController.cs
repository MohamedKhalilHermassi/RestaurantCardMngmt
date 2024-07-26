using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using RM.DemandeCarteResto.Business.Commands;
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
        private readonly NotificationCommands notificationCommands;
        private readonly NotificationQueries notifcationQueries; 
        private readonly HubConnection _hubConnection;
        AcceptDemandCardCommand _acceptDemandCardCommand;
        AddDemandCardCommand _addDemandCardCommand;
        RejectDemandCardCommand _rejectDemandCardCommand;
        RemoveDemandCardCommand _removeDemandCardCommand;
        UpdateCardRestoCommand _updateCardRestoCommand;

        public DemandeCardCommandsController(
            NotificationCommands _notificationCommands,
            AcceptDemandCardCommand acceptDemandCardCommand,
            AddDemandCardCommand addDemandCardCommand,
            RejectDemandCardCommand rejectDemandCardCommand,
            RemoveDemandCardCommand removeDemandCardCommand,
            UpdateCardRestoCommand updateCardRestoCommand
            )
        {
            _addDemandCardCommand = addDemandCardCommand;
            _acceptDemandCardCommand = acceptDemandCardCommand;
            _rejectDemandCardCommand = rejectDemandCardCommand;
            _removeDemandCardCommand = removeDemandCardCommand;
            _updateCardRestoCommand = updateCardRestoCommand;
            var hubUrl = "https://localhost:7279/notificationHub";
            notificationCommands = _notificationCommands;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

        }

        [HttpPost]
        public async Task<ActionResult<DemandeCarteRestaurant>> addDemandeCarteResto(DemandeCarteRestaurant demandeCard)
        {
            var id = Guid.NewGuid();
           var res =  await _addDemandCardCommand.ExecuteAsync(demandeCard);
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
               // await notificationCommands.addNotification(newNotif);

               // await _hubConnection.StartAsync(); 
               // await _hubConnection.InvokeAsync("ReceiveMessage");


                return CreatedAtAction(nameof(addDemandeCarteResto), new { id = demandeCard.Id }, demandeCard);

            }
        }
                
        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> deleteDemandeCard(string partitionkey)
        {
            await _removeDemandCardCommand.ExecuteAsync(partitionkey);
            return NoContent();
        }

        [HttpPut("{partitionkey}")]
        public async Task<IActionResult> updateDemandeCard(string partitionkey, DemandeCarteRestaurant demandeCard)
        {
            await _updateCardRestoCommand.ExecuteAsync(partitionkey, demandeCard);
            return NoContent();

        }
        [HttpPut("approve/{partitionkey}")]
      //  [Authorize(Roles ="Admin")]
        public async Task<IActionResult> approveDemandeCard(string partitionkey)
        {
            await _acceptDemandCardCommand.ExecuteAsync(partitionkey);

           // await _hubConnection.StartAsync();
           // await _hubConnection.InvokeAsync("NotifyUserAccept",foundDemand.UserEmail);
           // await _emailService.SendSimpleMessageAsync();
            return NoContent();

        }
        [HttpPut("reject/{partitionkey}")]
      //  [Authorize(Roles = "Admin")]

        public async Task<IActionResult> rejectDemandeCard(string partitionkey)
        {
            await _rejectDemandCardCommand.ExecuteAsync(partitionkey);
         //   await _hubConnection.StartAsync();
         //   await _hubConnection.InvokeAsync("NotifyUserReject",foundDemand.UserEmail);
         //   await _emailService.SendSimpleMessageAsyncReject();
            return NoContent();
        }
    }
}
