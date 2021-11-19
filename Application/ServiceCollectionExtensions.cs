using Application.Configuration.CommandsAndQueries.Commands;
using Application.Configuration.CommandsAndQueries.Commands.Interfaces;
using Application.Configuration.CommandsAndQueries.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddTransient<ICreateConfigurationCommand, CreateConfigurationCommand>()
                .AddTransient<IGetConfigurationQuery, GetConfigurationQuery>();
        }
    }
}
