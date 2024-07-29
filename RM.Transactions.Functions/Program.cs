using Abstraction;
using Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()

    .ConfigureFunctionsWebApplication() 
    .ConfigureServices((hostContext, services) =>
    {
    

        // Configure DbContext with Cosmos DB
        services.AddDbContext<EmailNotificationContext>(options =>
        {
            options.UseCosmos("https://khalilhermassi.documents.azure.com:443/", "2QcU5EE3ihmJPyWE2JAGdY49HXozhn7Ou3JORTjGQ6QzDcorzPmVtCxJxXw2UkDgSzXZzQBDsHARACDbZChnog==", "RestaurantCardDb");
        });

        // Register services
        services.AddScoped<IEmailNotificationRepository, EmailNotificationRepository>();

        // Add Application Insights
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
