using RM.Notif.Abstraction.Queries;
using RM.Notif.Abstraction.Repository;
using RM.Notif.Model.Entities;

namespace RM.Notif.Business.Queries
{
    public class NotificationQueries : INotificationQueries
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationQueries(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> getAllNotificationByReceiverId(string ReceiverId)
        {
            return await notificationRepository.getAllNotificationByReceiverId(ReceiverId);
        }
    }
}
