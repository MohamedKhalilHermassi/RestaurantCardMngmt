using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business;
using Model;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        ReadNotificationCommand _readNotificationCommand;
        GetAllNotificationsByReceiverIdQuery _getAllNotificationByReceiverId;

        public NotificationController(ReadNotificationCommand readNotificationCommand, GetAllNotificationsByReceiverIdQuery getAllNotificationByReceiverId)
        {
            _readNotificationCommand = readNotificationCommand;
            _getAllNotificationByReceiverId = getAllNotificationByReceiverId;
        }

        [HttpGet("{admin}")]
        [Authorize(Roles ="Admin")]
        public async Task<IEnumerable<Notification>> getNotificationForAdmin(string admin)
        {
            return await _getAllNotificationByReceiverId.ExecuteAsync(admin);
        }

      
        [HttpPut("readNotif/{partitionkey}")]
        public async Task readNotification(string partitionkey)
        {
                await _readNotificationCommand.ExecuteAsync(partitionkey);
        }
    }
}
