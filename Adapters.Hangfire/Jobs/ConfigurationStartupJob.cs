using System.Threading.Tasks;
using Adapters.Hangfire.Interfaces;
using Application.Configuration.CommandsAndQueries.Commands.Interfaces;

namespace Adapters.Hangfire.Jobs
{
    public class ConfigurationStartupJob : IConfigurationStartupJob
    {
        private readonly ICreateConfigurationCommand _createConfigurationCommand;

        public ConfigurationStartupJob(ICreateConfigurationCommand createConfigurationCommand)
        {
            _createConfigurationCommand = createConfigurationCommand;
        }

        public async Task Execute()
        {
            await _createConfigurationCommand.ExecuteAsync();
        }
    }
}
