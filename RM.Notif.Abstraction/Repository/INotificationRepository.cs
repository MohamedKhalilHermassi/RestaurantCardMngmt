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
        Task<IEnumerable<Notification>> GetAllNotificationByReceiverId(string ReceiverId);
        Task<Notification> AddNotification(Notification notification);
        Task ReadNotification(string PartitionKey);


    }
}
