using RM.Notifications.Abstraction;
using RM.Notifications.Model;

namespace RM.Notifications.Business
{
    public class GetAllNotificationsByReceiverIdQuery
    {
        private readonly INotificationRepository _notificationRepository;

        public GetAllNotificationsByReceiverIdQuery(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<IEnumerable<Notification>> ExecuteAsync(string partitionKey)
        {
            return await _notificationRepository.GetAllNotificationByReceiverId(partitionKey);

        }
    }
}
