using RM.Notifications.Abstraction;

namespace RM.Notifications.Business
{
    public class SendEmailCommand
    {
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        public SendEmailCommand(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        }
        public async Task ExecuteAsync(string receiverEmail, string notifPartitionKey)
        {
            await _emailNotificationRepository.SendEmailNotification(receiverEmail, notifPartitionKey);
        }
    }
}
