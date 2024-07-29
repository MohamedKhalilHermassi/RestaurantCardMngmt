using Microsoft.Extensions.DependencyInjection;
using Remote;

public static class GrpcConfig
{
    public static IServiceCollection AddCarteRestoGrpcClient(this IServiceCollection services)
    {
        services.AddGrpcClient<ICarteRestoService>(o =>
        {
            o.Address = new Uri("https://localhost:7093");
        }).ConfigureChannel(o =>
        {
        });

        return services;
    }
}
