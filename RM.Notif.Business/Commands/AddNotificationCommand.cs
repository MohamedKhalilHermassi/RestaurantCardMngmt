using RM.Notifications.Abstraction;
using RM.Notifications.Model;

namespace RM.Notifications.Business
{
    public class AddNotificationCommand
    {
        #region Fields
        private readonly INotificationRepository _notificationRepository;

        #endregion
        #region Constructeur
        public AddNotificationCommand(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        } 
        #endregion

        public async Task<Notification> ExecuteAsync(Notification notification)
        {
            return await _notificationRepository.AddNotification(notification);
        }

    }
}
