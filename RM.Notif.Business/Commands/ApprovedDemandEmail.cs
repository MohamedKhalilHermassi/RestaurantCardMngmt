using RM.Notifications.Abstraction;

namespace RM.Notif.Business
{
    public class ApprovedDemandEmail
    {
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        public ApprovedDemandEmail(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        }

        public async Task ExecuteAsync(string receiverEmail)
        {
            await _emailNotificationRepository.ApprovedDemandEmail(receiverEmail);
        }

    }
}
