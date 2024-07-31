using RM.Notifications.Abstraction;
using RM.Notifications.Model;
using System.Net.Mail;
using System.Net;


namespace RM.Notifications.Data
{
    public class EmailNotificationRepository : IEmailNotificationRepository
    {
        private readonly EmailNotificationContext _context;

        public EmailNotificationRepository(EmailNotificationContext context)
        {
            _context = context;
        }

        public Task<EmailNotification> GetEmailNotificationById(string PartitionKey)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailNotification(string ReceiverEmail, string NotifId)
        {
            
            string fromMail = "khalilherma7@gmail.com"; 
            string fromPassword = "zbbwjqqhtknmfvnp";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Alerte de Transactions éffectuées avec votre carte Restaurant";
            message.To.Add(new MailAddress(ReceiverEmail));
            message.Body = "<html><body>Vous n'avez pas effectué de transactions depuis plus de 2 mois.</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            
                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                  /* emailNotif.StatutError = ex.Message;
                   _context.SaveChanges();*/


                }
            
        }
        public async Task DemandSuccesfullyAddedEmail(string ReceiverEmail)
        {

            string fromMail = "khalilherma7@gmail.com";
            string fromPassword = "zbbwjqqhtknmfvnp";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Dépôt d'une nouvelle demande de carte restaurant";
            message.To.Add(new MailAddress(ReceiverEmail));
            message.Body = $"<html><body>Bonjour {ReceiverEmail}, votre demande de carte restaurant a été déposée avec succès. Elle va être traitée par un administrateur dans les plus brefs délais.</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            try
            {
               smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                /* emailNotif.StatutError = ex.Message;
                 _context.SaveChanges();*/


            }


        }

        public async Task ApprovedDemandEmail(string ReceiverEmail)
        {

            string fromMail = "khalilherma7@gmail.com";
            string fromPassword = "zbbwjqqhtknmfvnp";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Votre demande de carte restaurant a été acceptée.";
            message.To.Add(new MailAddress(ReceiverEmail));
            message.Body = $"<html><body>Bonjour {ReceiverEmail}, votre demande de carte restaurant a été acceptée. Veuillez visiter votre espace employé afin de consulter les détails de votre carte.</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            try
            {
               await smtpClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                /* emailNotif.StatutError = ex.Message;
                 _context.SaveChanges();*/


            }


        }
    }
}
