using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

public class CustomEmailProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
    }
}