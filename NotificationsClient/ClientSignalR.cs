using Microsoft.AspNetCore.SignalR.Client;
using Business;
using Model;

namespace NotificationsClient
{
    public class ClientSignalR
    {
        private readonly HubConnection _hubConnection;
        private readonly AddNotificationCommand _addNotificaitonCommand;
        private readonly ReadNotificationCommand _readNotificationCommand;
        
        public ClientSignalR(AddNotificationCommand addNotificaitonCommand, ReadNotificationCommand readNotificationCommand)
        {
            _addNotificaitonCommand = addNotificaitonCommand;
            _readNotificationCommand = readNotificationCommand;
            var hubUrl = "https://localhost:7279/notificationHub";
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
            
        }
        public async Task AdminAlert(Notification notification)
        {
             await _addNotificaitonCommand.ExecuteAsync(notification);
             await _hubConnection.StartAsync(); 
             await _hubConnection.InvokeAsync("ReceiveMessage");
        }
        public async Task EmployeePositiveAlert(string employeeEmail)
        {
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync("NotifyUserAccept",employeeEmail);
        }

        public async Task EmployeeNegativeAlert(string employeeEmail)
        {
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync("NotifyUserReject",employeeEmail);
        }


    }
}
