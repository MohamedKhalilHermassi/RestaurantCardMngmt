using Model;

namespace Abstraction
{
    public interface IEmailNotificationRepository
    {
        Task<EmailNotification> GetEmailNotificationById(string partitionKey);
        Task SendEmailNotification(string ReceiverEmail,string NotifId);
    }
}
