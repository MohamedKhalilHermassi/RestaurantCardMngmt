using RM.Notif.Abstraction.Repository;

namespace RM.Notif.Business.Commands
{
    public class ReadNotificationCommand
    {
        private readonly INotificationRepository _notificationRepository;
        public ReadNotificationCommand(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task ExecuteAsync(string partitionKey)
        {
            await _notificationRepository.ReadNotification(partitionKey);
        }
    }
}
