using Microsoft.Extensions.DependencyInjection;
using NotificationsClient;

namespace NotificationsClient.Extension
{

    public static class ConfigNotifClientService
    {
        public static IServiceCollection AddNotifClientServices(this IServiceCollection services)
        {
            services.AddScoped<ClientSignalR>();

            return services;
        }
    }
}