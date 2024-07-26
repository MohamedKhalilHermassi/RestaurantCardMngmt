using Microsoft.Extensions.DependencyInjection;
using RM.CarteResto.Abstraction.Repositories;
using RM.CarteResto.Business.Commands;
using RM.CarteResto.Business.Queries;
using RM.CarteResto.Data.Repository;
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Business;
using RM.Transaction.Remote.Contracts;
using RM.Transaction.Remote.Extension;
using RM.Transaction.Service.Services;

public static class ConfigCarteResto
{
    public static IServiceCollection AddCarteRestoServices(this IServiceCollection services)
    {
        // REPO
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ICarteRestoRepository, CarteRestoRepository>();
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
