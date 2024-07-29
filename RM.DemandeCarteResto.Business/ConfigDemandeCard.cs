using Abstraction;
using Business;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Remote;
using Service;


public static class ConfigDemandeCard
{
    public static IServiceCollection AddDemandCardServices(this IServiceCollection services)
    {
      
        //comment

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
