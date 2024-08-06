using Microsoft.Extensions.DependencyInjection;
using NotificationsClient;

namespace NotifCLient
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