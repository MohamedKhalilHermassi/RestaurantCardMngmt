using RM.Notifications.Abstraction;

namespace RM.Notif.Business.Commands
{
    public class RechargeCardEmail
    {
        #region Fields
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        #endregion
        #region Contructeur
        public RechargeCardEmail(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string receiverEmail)
        {
            await _emailNotificationRepository.RechargedCardEmail(receiverEmail);
        }
    }
}
