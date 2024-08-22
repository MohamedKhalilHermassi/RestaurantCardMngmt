using Microsoft.Extensions.DependencyInjection;
using RM.Notifications.Abstraction;
using RM.Notifications.Business;
using RM.Notifications.Data;

namespace EmailClient.Extension
{

    public static class ConfigEmailService
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services)
        {

            services.AddScoped<IEmailNotificationRepository, EmailNotificationRepository>();
            services.AddScoped<SendEmailCommand>();
            services.AddScoped<ClientEmail>();


            return services;
        }
    }
}