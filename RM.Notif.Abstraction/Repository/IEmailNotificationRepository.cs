using RM.Notifications.Model;

namespace RM.Notifications.Abstraction
{
    public interface IEmailNotificationRepository
    {
        Task<EmailNotification> GetEmailNotificationById(string partitionKey);
        Task SendEmailNotification(string ReceiverEmail,string NotifId);
        Task DemandSuccesfullyAddedEmail(string ReceiverEmail);
        Task ApprovedDemandEmail(string ReceiverEmail);
        Task RejectedDemandEmail(string ReceiverEmail);
    }
}
