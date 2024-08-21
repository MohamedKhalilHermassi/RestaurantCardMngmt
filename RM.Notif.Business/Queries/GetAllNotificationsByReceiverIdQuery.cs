using RM.Notifications.Abstraction;
using RM.Notifications.Model;

namespace RM.Notifications.Business
{
    public class GetAllNotificationsByReceiverIdQuery
    {
        #region Fields
        private readonly INotificationRepository _notificationRepository;

        #endregion
        #region Constructeur
        public GetAllNotificationsByReceiverIdQuery(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        } 
        #endregion
        public async Task<IEnumerable<Notification>> ExecuteAsync(string partitionKey)
        {
            return await _notificationRepository.GetAllNotificationByReceiverId(partitionKey);

        }
    }
}
