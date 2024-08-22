using RM.Notifications.Abstraction;

namespace RM.Notif.Business
{
    public class ApprovedDemandEmail
    {
        #region Fields
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        #endregion
        #region Constructeur
        public ApprovedDemandEmail(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string receiverEmail)
        {
            await _emailNotificationRepository.ApprovedDemandEmail(receiverEmail);
        }

    }
}
