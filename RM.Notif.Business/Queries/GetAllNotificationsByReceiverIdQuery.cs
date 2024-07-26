using RM.Notif.Abstraction.Repository;
using RM.Notif.Model.Entities;

namespace RM.Notif.Business.Queries
{
    public class GetAllNotificationsByReceiverIdQuery
    {
        private readonly INotificationRepository _notificationRepository;

        public GetAllNotificationsByReceiverIdQuery(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<IEnumerable<Notification>> ExecuteAsync(string role)
        {
            return await _notificationRepository.GetAllNotificationByReceiverId(partitionKey);

        }
    }
}
