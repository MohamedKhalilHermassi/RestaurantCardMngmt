using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RM.Notif.Business.Commands;
using RM.Notif.Business.Queries;
using RM.Notif.Model.Entities;

namespace RM.NotificationsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationQueries _notificationQueries;
        private readonly NotificationCommands _notificationCommands;


        public NotificationController(NotificationQueries notificationQueries, NotificationCommands notificationCommands)
        {
            _notificationQueries = notificationQueries;
            _notificationCommands = notificationCommands;
        }

        [HttpGet("{admin}")]
        [Authorize(Roles ="Admin")]
        public async Task<IEnumerable<Notification>> getNotificationForAdmin(string admin)
        {
            return await _notificationQueries.getAllNotificationByReceiverId(admin);
        }

      
        [HttpPut("readNotif/{partitionkey}")]
        public async Task readNotification(string partitionkey)
        {
                await _notificationCommands.readNotification(partitionkey);
        }
    }
}
