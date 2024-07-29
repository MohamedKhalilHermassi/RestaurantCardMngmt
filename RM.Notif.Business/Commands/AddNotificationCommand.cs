using Abstraction;
using Model;

namespace Business
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
