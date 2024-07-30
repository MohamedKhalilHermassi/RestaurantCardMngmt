using RM.Notifications.Model;

namespace RM.Notifications.Abstraction
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotificationByReceiverId(string ReceiverId);
        Task<Notification> AddNotification(Notification notification);
        Task ReadNotification(string PartitionKey);


    }
}
