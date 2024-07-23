using RM.Notif.Abstraction.Commands;
using RM.Notif.Abstraction.Repository;
using RM.Notif.Model.Entities;

namespace RM.Notif.Business.Commands
{
    public class NotificationCommands : INotificationCommands
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationCommands(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task<Notification> addNotification(Notification notification)
        {

           return  await notificationRepository.addNotification(notification);
        }

        public async Task readNotification(string partitionKey)
        {
             await notificationRepository.readNotification(partitionKey);   
        }
    }
}
