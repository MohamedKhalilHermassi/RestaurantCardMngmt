using Microsoft.Extensions.DependencyInjection;
using RM.CarteResto.Remote.Contracts;
using RM.CarteResto.Service.Services;
using RM.DemandeCarteResto.Abstraction.Repositories;
using RM.DemandeCarteResto.Business.Commands;
using RM.DemandeCarteResto.Business.Queries;
using RM.DemandeCarteResto.Data.Repository;


public static class ConfigDemaandeCard
{
    public static IServiceCollection AddDemandCardServices(this IServiceCollection services)
    {
        services.AddScoped<IDemandeCarteRestoRepository,DemandeCarteRestoRepository>();
        services.AddCarteRestoGrpcClient();
        services.AddScoped<ICarteRestoService, CarteRestoServiceGRPC>();
        services.AddScoped<AddDemandCardCommand>();
        services.AddScoped<AcceptDemandCardCommand>();
        services.AddScoped<RejectDemandCardCommand>();
        services.AddScoped<RemoveDemandCardCommand>();
        services.AddScoped<UpdateCardRestoCommand>();


        services.AddScoped<GetAllDemandesCardsQuery>();
        services.AddScoped<GetAllPendingDemandsQuery>();
        services.AddScoped<GetDemandCardByIdQuery>();
        services.AddScoped<GetDemandeCardByUserIdQuery>();

        return services;
    }
}
