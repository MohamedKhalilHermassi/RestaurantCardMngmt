using Microsoft.Extensions.DependencyInjection;


namespace Remote
{
    public static class GrpcConfigTransaction
    {
        public static IServiceCollection AddTransactionGrpcClient(this IServiceCollection services)
        {
            services.AddGrpcClient<ITransactionServiceContract>(o =>
            {
                o.Address = new Uri("https://localhost:7105");
            }).ConfigureChannel(o =>
            {
            });
            return services;
        }
    }
}
