using Microsoft.Extensions.DependencyInjection;

namespace Grpc.Performance.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection services)
        {
            return services.AddSingleton<IBigRepository, BigRepository>();
        } 
    }
}