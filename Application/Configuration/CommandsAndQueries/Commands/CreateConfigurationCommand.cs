using System;
using System.Threading.Tasks;
using Application.Configuration.CommandsAndQueries.Commands.Interfaces;
using Application.Configuration.Ports;
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
            var result = new Result<Guid>();

            var existingConfiguration = await _configurationRepository.GetAsync();

            if (existingConfiguration != null)
                return Result.Ok(existingConfiguration.Id.Value);

            var newConfigurationResult = ConfigurationDomain.CreateNew();

            if (newConfigurationResult.IsFailed)
                return result.WithErrors(newConfigurationResult.Errors);

            await _configurationRepository.InsertAsync(newConfigurationResult.Value);

            return Result.Ok(newConfigurationResult.Value.Id.Value);
        }
    }
}
