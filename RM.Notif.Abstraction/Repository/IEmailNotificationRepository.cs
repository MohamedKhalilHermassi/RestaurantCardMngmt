using RM.Notif.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Notif.Abstraction.Repository
{
    public interface IEmailNotificationRepository
    {
        Task<EmailNotification> GetEmailNotificationById(string partitionKey);
        Task SendEmailNotification(string ReceiverEmail,string NotifId);
    }
}
