using Microsoft.Extensions.DependencyInjection;
using Abstraction;
using Business;
using Data;

namespace EmailClient
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