using Microsoft.Extensions.DependencyInjection;
using RM.Transaction.Remote.Contracts;


namespace RM.Transaction.Remote.Extension
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
