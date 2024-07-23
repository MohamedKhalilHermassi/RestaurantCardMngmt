using RM.Notif.Model.Entities;

namespace RM.Notif.Abstraction.Commands
{
    public interface IEmailNotificationCommands
    {
        Task SendEmailNotification(string ReceiverEmail,string NotifId);

    }
}
