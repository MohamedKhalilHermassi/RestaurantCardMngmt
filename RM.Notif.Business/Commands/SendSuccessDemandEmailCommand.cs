using RM.Notifications.Abstraction;

namespace RM.Notif.Business
{
    public class SendSuccessDemandEmailCommand
    {
        #region Fields
        private readonly IEmailNotificationRepository _emailNotificationRepository;

        #endregion
        #region Constructeur
        public SendSuccessDemandEmailCommand(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        } 
        #endregion

        public async Task ExecuteAsync(string receiverEmail)
        {
            await _emailNotificationRepository.DemandSuccesfullyAddedEmail(receiverEmail);
        }

    }
}
