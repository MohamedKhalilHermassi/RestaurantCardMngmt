using Microsoft.Extensions.DependencyInjection;

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