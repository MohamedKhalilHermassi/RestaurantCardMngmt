using RM.Notif.Abstraction.Repository;
using RM.Notif.Model.Entities;

namespace RM.Notif.Business.Commands
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
