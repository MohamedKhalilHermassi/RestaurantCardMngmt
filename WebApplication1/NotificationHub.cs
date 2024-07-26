using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

public class NotificationHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();



    public override Task OnConnectedAsync()
    {
        var email = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (email != null)
        {
            _connections[email] = Context.ConnectionId;
            Console.WriteLine($"Connected: {email} - {Context.ConnectionId}");
        }
        else
        {
            Console.WriteLine("Email claim not found");
        }

        return base.OnConnectedAsync();
    }
    public override  Task OnDisconnectedAsync(Exception exception)
    {
        var email = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (email != null)
        {
            _connections.TryRemove(email, out _);
            Console.WriteLine($"Disconnected: {email}");
        }
        return base.OnDisconnectedAsync(exception);
    }

    public async Task ReceiveMessage()
    {
        if (_connections.TryGetValue("admin@admin.com", out string connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", "Une nouvelle demande de carte a été déposée");
            
        }
        else
        {
            Console.WriteLine($"No connection found for admin@admin.com");
        }
    }
    public async Task NotifyUserAccept(string userEmail)
    {
        if (_connections.TryGetValue(userEmail, out string connectionId))
        {
            await Clients.Client(connectionId).SendAsync("NotifyUserAccept", "Votre demande de carte restaurant a été acceptée.");

        }
        else
        {
            Console.WriteLine($"No connection found for ${userEmail}");
        }
    }
    public async Task NotifyUserReject(string userEmail)
    {
        if (_connections.TryGetValue(userEmail, out string connectionId))
        {
            await Clients.Client(connectionId).SendAsync("NotifyUserReject", "Votre demande de carte restaurant a été rejetée.");

        }
        else
        {
            Console.WriteLine($"No connection found for ${userEmail}");
        }
    }
}
