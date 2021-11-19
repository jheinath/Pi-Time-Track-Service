using Application.Configuration.CommandsAndQueries.Commands;
using Application.Configuration.CommandsAndQueries.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddTransient<ICreateConfigurationCommand, CreateConfigurationCommand>();
        }
    }
}
