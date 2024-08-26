using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RM.Notifications.Business;
using System.Text.Json;

namespace RM.Transactions.Functions.NewFolder
{
    public class SendNotificationFunction
    {
        private readonly ILogger<SendNotificationFunction> _logger;
        private readonly SendEmailCommand _sendEmailCommand;
        public SendNotificationFunction(ILogger<SendNotificationFunction> logger, SendEmailCommand sendEmailCommand)
        {
            _logger = logger;
            _sendEmailCommand = sendEmailCommand;
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
                await _sendEmailCommand.ExecuteAsync(userEmail, NotifId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
