using RM.Notif.Model.Entities;

namespace RM.Notif.Abstraction.Commands
{
    public interface INotificationCommands
    {
        Task<Notification> addNotification(Notification notification);
        Task readNotification(string partitionKey);


    }
}
