// Program.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using RM.CarteResto.Data.Data;
using RM.CarteResto.Model.Entitiy;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

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

                services.AddDbContext<CarteRestoContext>(options =>
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
    private readonly CarteRestoContext _context;

    public CarteRestoService(CarteRestoContext context)
    {
        _context = context;
    }

    public async Task Run()
    {
        await _context.Database.EnsureCreatedAsync();

        // Add a new Transaction
        var carte = new CarteRestaurant
        {

            Numero = "14.40",
            Solde = 140,
            TransactionIds = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
        };

        _context.CartesRestaurant.Add(carte);
        await _context.SaveChangesAsync();

        
    }
}
