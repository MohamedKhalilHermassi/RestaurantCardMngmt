using RM.Notifications.Abstraction;
using RM.Notifications.Model;
using System.Net.Mail;
using System.Net;


namespace RM.Notifications.Data
{
    public class EmailNotificationRepository : IEmailNotificationRepository
    {
        private readonly EmailNotificationContext _context;
        readonly string fromMail = "khalilherma7@gmail.com";
        readonly string fromPassword = "zbbwjqqhtknmfvnp";
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
        public async Task DemandSuccesfullyAddedEmail(string receiverEmail)
        {
            string htmlFilePath = "C:/Users/Khalil/Desktop/Domain Driven Design/emailTemplates/DemandeDeposeeEmail.html";
            string htmlContent = File.ReadAllText(htmlFilePath);

            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromMail),
                Subject = "Dépôt d'une nouvelle demande de carte restaurant",
                Body = htmlContent,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(receiverEmail));

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
                // emailNotif.StatutError = ex.Message;
                // _context.SaveChanges();
            }
        }



        public async Task ApprovedDemandEmail(string receiverEmail)
        {
            string htmlFilePath = "C:/Users/Khalil/Desktop/Domain Driven Design/emailTemplates/DemandeAccepteeEmail.html";
            string htmlContent = File.ReadAllText(htmlFilePath);

            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromMail),
                Subject = "Votre demande de carte restaurant a été acceptée.",
                Body = htmlContent,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(receiverEmail));

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
                // emailNotif.StatutError = ex.Message;
                // _context.SaveChanges();
            }
        }

        public async Task RejectedDemandEmail(string receiverEmail)
        {
            string htmlFilePath = "C:/Users/Khalil/Desktop/Domain Driven Design/emailTemplates/DemandeRefuseeEmail.html";
            string htmlContent = File.ReadAllText(htmlFilePath);

            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromMail),
                Subject = "Votre demande de carte restaurant a été rejetée.",
                Body = htmlContent,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(receiverEmail));

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
                // emailNotif.StatutError = ex.Message;
                // _context.SaveChanges();
            }
        }

        public async Task RechargedCardEmail(string receiverEmail)
        {
            string htmlFilePath = "C:/Users/Khalil/Desktop/Domain Driven Design/emailTemplates/CarteRechargeeEmail.html";
            string htmlContent = File.ReadAllText(htmlFilePath);

            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromMail),
                Subject = "Votre carte restaurant a été rechargée",
                Body = htmlContent,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(receiverEmail));

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
                // emailNotif.StatutError = ex.Message;
                // _context.SaveChanges();
            }
        }




    }
}