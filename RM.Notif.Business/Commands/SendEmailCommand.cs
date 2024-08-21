using RM.Notifications.Abstraction;

namespace RM.Notifications.Business
{
    public class SendEmailCommand
    {
        #region Fields
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        #endregion
        #region Constructeur
        public SendEmailCommand(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        } 
        #endregion
        public async Task ExecuteAsync(string receiverEmail, string notifPartitionKey)
        {
            await _emailNotificationRepository.SendEmailNotification(receiverEmail, notifPartitionKey);
        }
    }
}
