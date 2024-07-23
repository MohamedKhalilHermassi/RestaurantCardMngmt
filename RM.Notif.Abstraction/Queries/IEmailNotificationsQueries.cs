using RM.Notif.Model.Entities;

namespace RM.Notif.Abstraction.Queries
{
    public interface IEmailNotificationsQueries
    {
        Task<EmailNotification> GetEmailNotificationById(string partitionKey);

    }
}
