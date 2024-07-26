using Business;
using Microsoft.Extensions.DependencyInjection;
using RM.Transaction.Abstraction.Repositories;
using RM.Transaction.Business;

public static class ConfigTransactions
{
    public static IServiceCollection AddTransactionServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<AddTransactionCommand>();
        services.AddScoped<RemoveTransactionCommand>();
        services.AddScoped<UpdateTransactionCommand>();

        services.AddScoped<GetTransactionQuery>();
        services.AddScoped<GetAllTransactionsQuery>();
        services.AddScoped<GetTransactionsByCardQuery>();

        return services;
    }
}
