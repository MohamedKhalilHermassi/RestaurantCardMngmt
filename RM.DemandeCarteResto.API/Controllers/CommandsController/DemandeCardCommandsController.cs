using EmailClient;
using Microsoft.AspNetCore.Mvc;
using NotificationsClient;
using RM.DemandeCarteResto.Business;    
using RM.DemandeCarteResto.Model;
using Microsoft.AspNetCore.Authorization;
using RM.Notifications.Business;
using RM.Notifications.Model;

namespace RM.DemandeCarteResto.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeCardCommandsController : ControllerBase
    {
        private readonly AcceptDemandCardCommand _acceptDemandCardCommand;
        private readonly AddDemandCardCommand _addDemandCardCommand;
        private readonly RejectDemandCardCommand _rejectDemandCardCommand;
        private readonly RemoveDemandCardCommand _removeDemandCardCommand;
        private readonly UpdateCardRestoCommand _updateCardRestoCommand;
        private readonly AddNotificationCommand _addNotificationCommand;
        private readonly ClientEmail _clientEmail;
        private readonly ClientSignalR _clientSignalR;
        public DemandeCardCommandsController(
            AcceptDemandCardCommand acceptDemandCardCommand,
            AddNotificationCommand addNotificationCommand,
            AddDemandCardCommand addDemandCardCommand,
            RejectDemandCardCommand rejectDemandCardCommand,
            RemoveDemandCardCommand removeDemandCardCommand,
            UpdateCardRestoCommand updateCardRestoCommand,
            ClientEmail clientEmail,
            ClientSignalR clientSignalR
            )
        {
            _addDemandCardCommand = addDemandCardCommand;
            _acceptDemandCardCommand = acceptDemandCardCommand;
            _rejectDemandCardCommand = rejectDemandCardCommand;
            _removeDemandCardCommand = removeDemandCardCommand;
            _updateCardRestoCommand = updateCardRestoCommand;
            _addNotificationCommand = addNotificationCommand;
            _clientEmail = clientEmail;
            _clientSignalR = clientSignalR;



        }

        /// <summary>
        /// Créer une nouvelle demande de carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de créer une nouvelle demande de carte restaurant pour un utilisateur spécifié.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<DemandeCarteRestaurant>> AddDemandeCarteResto(DemandeCarteRestaurant demandeCard)
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

                // await _hubConnection.StartAsync(); 
                // await _hubConnection.InvokeAsync("ReceiveMessage");

               // await _clientEmail.SendEmail("khalilherma6@gmail.com", "1234");
                await _clientSignalR.AdminAlert(newNotif);
                return CreatedAtAction(nameof(AddDemandeCarteResto), new { id = demandeCard.Id }, demandeCard);

            }
        }
        /// <summary>
        /// Supprimer une demande de carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de supprimer une demande de carte restaurant en spécifiant son identifiant.
        /// </remarks>       
        [HttpDelete("{partitionkey}")]
        public async Task<IActionResult> DeleteDemandeCard(string partitionkey)
        {
            await _removeDemandCardCommand.ExecuteAsync(partitionkey);
            return NoContent();
        }
        /// <summary>
        /// Modifier une demande de carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de modifier une demande de carte restaurant en spécifiant son identifiant.
        /// </remarks>   
        [HttpPut("{partitionkey}")]
        public async Task<IActionResult> UpdateDemandeCard(string partitionkey, DemandeCarteRestaurant demandeCard)
        {
            await _updateCardRestoCommand.ExecuteAsync(partitionkey, demandeCard);
            return NoContent();

        }
        /// <summary>
        /// Accepter une demande de carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet d'accepter une demande de carte restaurant et cette action entraine la création d'une nouvelle carte restaurant via gRPC.
        /// </remarks>   
        [HttpPut("approve/{partitionkey}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ApproveDemandeCard(string partitionkey)
        {
            await _acceptDemandCardCommand.ExecuteAsync(partitionkey);

           // await _hubConnection.StartAsync();
           // await _hubConnection.InvokeAsync("NotifyUserAccept",foundDemand.UserEmail);
           // await _emailService.SendSimpleMessageAsync();
            return NoContent();

        }
        /// <summary>
        /// Rejeter une demande de carte restaurant
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de rejeter une demande de carte restaurant spécifique.
        /// </remarks>   
        [HttpPut("reject/{partitionkey}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RejectDemandeCard(string partitionkey)
        {
            await _rejectDemandCardCommand.ExecuteAsync(partitionkey);
         //   await _hubConnection.StartAsync();
         //   await _hubConnection.InvokeAsync("NotifyUserReject",foundDemand.UserEmail);
         //   await _emailService.SendSimpleMessageAsyncReject();
            return NoContent();
        }
    }
}
