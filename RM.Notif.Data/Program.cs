using RM.Notifications.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RM.Notifications.Model;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                var cosmosDbSettings = hostContext.Configuration.GetSection("CosmosDb");
                var accountEndpoint = cosmosDbSettings["AccountEndpoint"];
                var accountKey = cosmosDbSettings["AccountKey"];
                var databaseName = cosmosDbSettings["DatabaseName"];

                services.AddDbContext<NotificationContext>(options =>
                {
                    options.UseCosmos(accountEndpoint, accountKey, databaseName);
                });
                services.AddDbContext<EmailNotificationContext>(options =>
                {
                    options.UseCosmos(accountEndpoint, accountKey, databaseName);
                });
                services.AddScoped<NotificationService>();
            });

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var transactionService = services.GetRequiredService<NotificationService>();

            await transactionService.Run();
        }
    }
}

public class NotificationService
{
    private readonly NotificationContext _context;
    public NotificationService(NotificationContext context)
    {
        _context = context;
    }

    public async Task Run()
    {
        await _context.Database.EnsureCreatedAsync();
       var id=Guid.NewGuid();
        var notification = new Notification
        {
          NotificationId = id,
          PartitionKey= id.ToString(),
            Message="hello notif",
            Read=false,
            ReceiverId= "4690cf9c-1bd9-4216-8427-9b302a630960"


        };


        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();


    }
}
