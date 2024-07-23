using RM.Notif.Model.Entities;

namespace RM.Notif.Abstraction.Queries
{
    public interface INotificationQueries
    {
        Task<IEnumerable<Notification>> getAllNotificationByReceiverId(string ReceiverId);

    }
}
