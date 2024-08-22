using RM.Notifications.Abstraction;

namespace RM.Notifications.Business
{
    public class ReadNotificationCommand
    {
        #region Fields
        private readonly INotificationRepository _notificationRepository;

        #endregion
        #region Constructeur
        public ReadNotificationCommand(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string partitionKey)
        {
            await _notificationRepository.ReadNotification(partitionKey);
        }
    }
}
