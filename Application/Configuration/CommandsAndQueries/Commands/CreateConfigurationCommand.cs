using System;
using System.Threading.Tasks;
using Adapters.Database.Repositories.ConfigurationRepository;
using Application.Configuration.CommandsAndQueries.Commands.Interfaces;
using FluentResults;
using ConfigurationDomain = Domain.Aggregates.Configuration.Configuration;

namespace Application.Configuration.CommandsAndQueries.Commands
{
    public class CreateConfigurationCommand : ICreateConfigurationCommand
    {
        private readonly IConfigurationRepository _configurationRepository;

        public CreateConfigurationCommand(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<Result<Guid>> ExecuteAsync()
        {
            var newConfiguration = ConfigurationDomain.Create();

            await _configurationRepository.InsertAsync(newConfiguration);

            return Result.Ok(newConfiguration.Id);
        }
    }
}
