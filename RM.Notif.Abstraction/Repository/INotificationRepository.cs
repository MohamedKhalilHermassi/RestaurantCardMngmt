using RM.Notif.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Notif.Abstraction.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> getAllNotificationByReceiverId(string ReceiverId);
        Task<Notification> addNotification(Notification notification);
        Task readNotification(string PartitionKey);


    }
}
