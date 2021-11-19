using Adapters.Hangfire.Interfaces;
using Adapters.Hangfire.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Hangfire
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfireServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IConfigurationStartupJob, ConfigurationStartupJob>()
                .AddTransient<IHangfireStartup, HangfireStartup>();
        }
    }
}
