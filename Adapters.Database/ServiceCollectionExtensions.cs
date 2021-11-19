using Adapters.Database.Install;
using Adapters.Database.Repositories.ConfigurationRepository;
using Application.Configuration.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            return services.AddTransient<IConfigurationRepository, ConfigurationRepository>()
                .AddTransient<IDbSetup, DbSetup>();
        }
    }
}
