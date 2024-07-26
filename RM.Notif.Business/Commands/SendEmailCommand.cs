using RM.Notif.Abstraction.Repository;

namespace RM.Notif.Business.Commands
{
    public class SendEmailCommand
    {
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        public SendEmailCommand(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        }
        public async Task ExecuteAsync(string ReceiverEmail, string NotifId)
        {
            await _emailNotificationRepository.SendEmailNotification(ReceiverEmail, NotifId);
        }
    }
}
