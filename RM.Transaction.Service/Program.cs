using Microsoft.EntityFrameworkCore;
using RM.Transaction.Data.Data;
using RM.Transaction.Service.Services;
using ProtoBuf.Grpc.Server;
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Business;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
var cosmosDbSettings = builder.Configuration.GetSection("CosmosDb");
var accountEndpoint = cosmosDbSettings["AccountEndpoint"];
var accountKey = cosmosDbSettings["AccountKey"];
var databaseName = cosmosDbSettings["DatabaseName"];
builder.Services.AddDbContext<TransactionContext>(options =>
{
    options.UseCosmos(accountEndpoint, accountKey, databaseName);
});

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<TransactionServiceGRPC>();
app.MapGrpcReflectionService();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
