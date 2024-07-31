using RM.Notifications.Abstraction;

namespace RM.Notif.Business
{
    public class SendSuccessDemandEmailCommand
    {
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        public SendSuccessDemandEmailCommand(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        }

        public async Task ExecuteAsync(string receiverEmail)
        {
            await _emailNotificationRepository.DemandSuccesfullyAddedEmail(receiverEmail);
        }

    }
}
