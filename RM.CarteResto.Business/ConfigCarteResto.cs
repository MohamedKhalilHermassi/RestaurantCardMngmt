using RM.CarteResto.Abstraction;
using RM.CarteResto.Business;
using RM.CarteResto.Data;
using Microsoft.Extensions.DependencyInjection;
using RM.Transaction.Remote;
using RM.Transaction.Service;
using RM.Transaction.Abstraction;
using RM.Transaction.Data;

public static class ConfigCarteResto
{
    public static IServiceCollection AddCarteRestoServices(this IServiceCollection services)
    {
        // REPO
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ICarteRestoRepository, CarteRestoRepository>();
        
        // GRPC 
        services.AddTransactionGrpcClient();
        services.AddScoped<TransactionServiceGRPC>();
        services.AddScoped<ITransactionServiceContract, TransactionServiceGRPC>();

        // COMMANDS
        services.AddScoped<AddCardCommand>();
        services.AddScoped<ChargeCardCommand>();
        services.AddScoped<DischargeCardCommand>();
        services.AddScoped<RemoveCardCommand>();
        services.AddScoped<UpdateCardCommand>();

        // QUERIES
        services.AddScoped<GetAllCardsQuery>();
        services.AddScoped<GetCardByUserIdQuery>();
        services.AddScoped<GetCardQuery>();

        return services;
    }
}
