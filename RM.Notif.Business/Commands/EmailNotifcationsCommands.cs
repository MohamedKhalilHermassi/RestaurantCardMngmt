using RM.Notif.Abstraction.Commands;
using System.Net.Mail;
using System.Net;
using RM.Notif.Abstraction.Repository;

namespace RM.Notif.Business.Commands
{
    public class EmailNotifcationsCommands : IEmailNotificationCommands
    {
        private readonly IEmailNotificationRepository _repository;

        public EmailNotifcationsCommands(IEmailNotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task SendEmailNotification(string ReceiverEmail, string NotifId)
        {
          await _repository.SendEmailNotification(ReceiverEmail,NotifId);
        }
    }
}
