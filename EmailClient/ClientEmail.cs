using RM.Notifications.Business;

namespace EmailClient
{
    public class ClientEmail
    {
        private readonly SendEmailCommand _sendEmailCommand;

        public ClientEmail(SendEmailCommand sendEmailCommand)
        {
            _sendEmailCommand = sendEmailCommand;
        }

        public async Task SendEmail(string receiverEmail, string notifPartitionKey)
        {
            await _sendEmailCommand.ExecuteAsync(receiverEmail, notifPartitionKey);
        }

    }
}
