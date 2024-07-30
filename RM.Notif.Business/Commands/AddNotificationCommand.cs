using RM.Notifications.Abstraction;
using RM.Notifications.Model;

namespace RM.Notifications.Business
{
    public class AddNotificationCommand
    {
        private readonly INotificationRepository _notificationRepository;

        public AddNotificationCommand(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> ExecuteAsync(Notification notification)
        {
            return await _notificationRepository.AddNotification(notification);
        }

    }
}
