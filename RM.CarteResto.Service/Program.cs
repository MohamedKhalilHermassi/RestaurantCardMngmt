using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Data.Data;
using RM.CarteResto.Data.Repository;
using RM.CarteResto.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
var cosmosDbSettings = builder.Configuration.GetSection("CosmosDb");
var accountEndpoint = cosmosDbSettings["AccountEndpoint"];
var accountKey = cosmosDbSettings["AccountKey"];
var databaseName = cosmosDbSettings["DatabaseName"];
builder.Services.AddDbContext<CarteRestoContext>(options =>
{
    options.UseCosmos (accountEndpoint, accountKey, databaseName);
});

builder.Services.AddScoped<ICarteRestoRepository, CarteRestoRepository>();
builder.Services.AddCodeFirstGrpc();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CarteRestoServiceGRPC>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
