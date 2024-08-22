using Microsoft.Extensions.DependencyInjection;
using RM.Notif.Business;
using RM.Notif.Business.Commands;
using RM.Notifications.Abstraction;
using RM.Notifications.Business;
using RM.Notifications.Data;

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
        services.AddScoped<SendSuccessDemandEmailCommand>();
        services.AddScoped<ApprovedDemandEmail>();
        services.AddScoped<RejectedDemandEmail>();
        services.AddScoped<RechargeCardEmail>();


        // QUERIES
        services.AddScoped<GetAllNotificationsByReceiverIdQuery>();

        return services;
    }
}
