using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RM.Notif.Business.Commands;
using System.Text.Json;

namespace RM.Notif.Functions
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly EmailNotifcationsCommands _emailNotifcationsCommands;
        public Function1(ILogger<Function1> logger, EmailNotifcationsCommands emailNotifcationsCommands)
        {
            _logger = logger;
            _emailNotifcationsCommands = emailNotifcationsCommands;
        }

        [Function("Function1")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var jsonDoc = JsonDocument.Parse(requestBody);
                var root = jsonDoc.RootElement;
                string NotifId = root.GetProperty("id").GetString();
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
