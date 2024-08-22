using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.Notifications.Business;
using RM.Notifications.Model;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        #region Fields
        ReadNotificationCommand _readNotificationCommand;
        GetAllNotificationsByReceiverIdQuery _getAllNotificationByReceiverId;
        #endregion

        #region Constructeur
        public NotificationController(ReadNotificationCommand readNotificationCommand, GetAllNotificationsByReceiverIdQuery getAllNotificationByReceiverId)
        {
            _readNotificationCommand = readNotificationCommand;
            _getAllNotificationByReceiverId = getAllNotificationByReceiverId;
        } 
        #endregion
        /// <summary>
        /// Retourner la liste de notifications
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de retourner la liste de notifications destinée aux administrateurs.
        /// </remarks>   
        [HttpGet("{admin}")]
        [Authorize(Roles ="Admin")]
        public async Task<IEnumerable<Notification>> GetNotificationForAdmin(string admin)
        {
            return await _getAllNotificationByReceiverId.ExecuteAsync(admin);
        }

        /// <summary>
        /// Modifier une notification
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de rendre le statut "lu" d'une notification "vrai". 
        /// </remarks>  
        [HttpPut("readNotif/{partitionkey}")]
        public async Task ReadNotification(string partitionkey)
        {
                await _readNotificationCommand.ExecuteAsync(partitionkey);
        }
    }
}
