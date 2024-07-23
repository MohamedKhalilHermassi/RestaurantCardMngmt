using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RM.DemandeCarteResto.Data.Data;
using RM.Notif.Abstraction.Repository;
using RM.Notif.Business.Commands;
using RM.Notif.Data.Repository;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()

     .ConfigureAppConfiguration((hostingContext, config) =>
     {
         config.SetBasePath(Directory.GetCurrentDirectory());
         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
         config.AddEnvironmentVariables();
     })
     .ConfigureServices((hostContext,services)=>
     {
         var cosmosDbSettings = hostContext.Configuration.GetSection("CosmosDb");
         var accountEndpoint = cosmosDbSettings["AccountEndpoint"];
         var accountKey = cosmosDbSettings["AccountKey"];
         var databaseName = cosmosDbSettings["DatabaseName"];
         services.AddDbContext<EmailNotificationContext>(options =>
         {
             options.UseCosmos(accountEndpoint, accountKey, databaseName);
         });
         services.AddScoped<IEmailNotificationRepository, EmailNotificationRepository>();
         services.AddScoped<EmailNotifcationsCommands>();
 
         services.AddApplicationInsightsTelemetryWorkerService();
             services.ConfigureFunctionsApplicationInsights();
     })
    .Build();

host.Run();
