using RM.DemandeCarteResto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RM.DemandeCarteResto.Model;

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

                services.AddDbContext<DemandeCarteRestoContext>(options =>
                {
                    options.UseCosmos(accountEndpoint, accountKey, databaseName);
                });

                services.AddScoped<CarteRestoService>();
            });

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var transactionService = services.GetRequiredService<CarteRestoService>();

            await transactionService.Run();
        }
    }
}

public class CarteRestoService
{
    private readonly DemandeCarteRestoContext _context;

    public CarteRestoService(DemandeCarteRestoContext context)
    {
        _context = context;
    }

    public async Task Run()
    {
        await _context.Database.EnsureCreatedAsync();
        string guidString = "12345678-1234-1234-1234-123456789abc";
        Guid myGuid = new Guid(guidString);
        // Add a new Transaction
        var demandeCarte = new DemandeCarteRestaurant
        {
            Date = DateTime.Now,
            Status = false,
            UserId = myGuid.ToString()

        };

        _context.DemandesCarteRestaurant.Add(demandeCarte);
        await _context.SaveChangesAsync();


    }
}
