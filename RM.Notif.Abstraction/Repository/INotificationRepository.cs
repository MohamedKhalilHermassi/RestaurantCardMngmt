using Model;

namespace Abstraction
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotificationByReceiverId(string ReceiverId);
        Task<Notification> AddNotification(Notification notification);
        Task ReadNotification(string PartitionKey);


    }
}
