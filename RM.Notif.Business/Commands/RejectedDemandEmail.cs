using RM.Notifications.Abstraction;

namespace RM.Notif.Business.Commands
{
    public class RejectedDemandEmail
    {
        #region Fields
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        #endregion
        #region Constructeur
        public RejectedDemandEmail(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string email)
        {
            await _emailNotificationRepository.RejectedDemandEmail(email);
        }
    }
}
