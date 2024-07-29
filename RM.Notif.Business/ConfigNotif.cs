using Microsoft.Extensions.DependencyInjection;
using Abstraction;
using Business;
using Data;

public static class ConfigTransactions
{
    public static IServiceCollection AddNotificationsServices(this IServiceCollection services)
    {
        // REPOS
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IEmailNotificationRepository, EmailNotificationRepository>();

        // COMMANDS
        services.AddScoped<AddNotificationCommand>();
        services.AddScoped<ReadNotificationCommand>();
        services.AddScoped<SendEmailCommand>();
        
        // QUERIES
        services.AddScoped<GetAllNotificationsByReceiverIdQuery>();

        return services;
    }
}
