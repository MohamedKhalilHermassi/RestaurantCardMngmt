using RM.Notifications.Abstraction;

namespace RM.Notif.Business.Commands
{
    public class RechargeCardEmail
    {
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        public RechargeCardEmail(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        }

        public async Task ExecuteAsync(string receiverEmail)
        {
            await _emailNotificationRepository.RechargedCardEmail(receiverEmail);
        }
    }
}
