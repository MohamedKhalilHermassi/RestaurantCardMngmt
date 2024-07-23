using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RM.User.Data.Data;
using RM.User.Model.Entities;

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

                services.AddDbContext<UserContext>(options =>
                {
                    options.UseCosmos(accountEndpoint, accountKey, databaseName);
                });

                services.AddScoped<TransactionService>();
            });

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var transactionService = services.GetRequiredService<TransactionService>();

            await transactionService.Run();
        }
    }
}

public class TransactionService
{
    private readonly UserContext _context;

    public TransactionService(UserContext context)
    {
        _context = context;
    }

    public async Task Run()
    {
        await _context.Database.EnsureCreatedAsync();

        // Add a new Transaction
        var transaction = new Utilisateur
        {

          Email="test@gmail.com",
          UserName="khalil"

        };

        _context.Users.Add(transaction);
        await _context.SaveChangesAsync();




    }
}
