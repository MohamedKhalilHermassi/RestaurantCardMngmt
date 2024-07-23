using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RM.Notif.Business.Commands;
using System.Text.Json;

namespace RM.Transactions.Functions
{
    public class SendNotificationFunction
    {
        private readonly ILogger<SendNotificationFunction> _logger;
       private readonly EmailNotifcationsCommands _emailNotifcationsCommands;
        public SendNotificationFunction(ILogger<SendNotificationFunction> logger, EmailNotifcationsCommands emailNotifcationsCommands)
        {
            _logger = logger;
           _emailNotifcationsCommands = emailNotifcationsCommands;
        }

        [Function("sendEmailNotif")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var jsonDoc = JsonDocument.Parse(requestBody);
                var root = jsonDoc.RootElement;
                string NotifId = root.GetProperty("Id").GetString();
                string userEmail = root.GetProperty("Email").GetString();
               await _emailNotifcationsCommands.SendEmailNotification(userEmail, NotifId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
