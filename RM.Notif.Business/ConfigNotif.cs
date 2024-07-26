using Microsoft.Extensions.DependencyInjection;
using RM.Notif.Abstraction.Repository;
using RM.Notif.Business.Commands;
using RM.Notif.Business.Queries;
using RM.Notif.Data.Repository;

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
