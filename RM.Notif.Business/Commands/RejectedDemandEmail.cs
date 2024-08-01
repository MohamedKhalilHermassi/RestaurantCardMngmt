using RM.Notifications.Abstraction;

namespace RM.Notif.Business.Commands
{
    public class RejectedDemandEmail
    {
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        public RejectedDemandEmail(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        }

        public async Task ExecuteAsync(string email)
        {
            await _emailNotificationRepository.RejectedDemandEmail(email);
        }
    }
}
