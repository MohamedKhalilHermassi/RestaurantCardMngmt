﻿using RM.Notif.Abstraction.Repository;
using RM.Notif.Model.Entities;
using System.Net.Mail;
using System.Net;
using RM.DemandeCarteResto.Data.Data;


namespace RM.Notif.Data.Repository
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
    }
}