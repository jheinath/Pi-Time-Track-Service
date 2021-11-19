using System.Threading.Tasks;
using Application.Configuration.CommandsAndQueries.Commands.Interfaces;
using Application.Configuration.Ports;
using Domain.Aggregates.Configuration.ValueObjects;
using FluentResults;

namespace Application.Configuration.CommandsAndQueries.Commands
{
    public class UpdateConfigurationCommand : IUpdateConfigurationCommand
    {
        private readonly IConfigurationRepository _configurationRepository;

        public UpdateConfigurationCommand(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<Result> ExecuteAsync(int vacationDays, decimal workingHoursPerDay, bool isEnabled, string accessToken)
        {
            var result = new Result();

            var existingConfiguration = await _configurationRepository.GetAsync();

            var vacationDaysCount = VacationDaysCount.Create(vacationDays);
            var workingHours = WorkingHoursPerDay.Create(workingHoursPerDay);
            var token = TogglTrackAccessToken.Create(accessToken);

            var mergedResult = Result.Merge(vacationDaysCount, workingHours, token);

            if (mergedResult.IsFailed)
                return result.WithErrors(mergedResult.Errors);

            var newConfigurationResult = existingConfiguration.Update(vacationDaysCount.Value, workingHours.Value, token.Value, isEnabled);

            if (newConfigurationResult.IsFailed)
                return result.WithErrors(newConfigurationResult.Errors);

            await _configurationRepository.UpdateAsync(existingConfiguration);

            return Result.Ok();
        }
    }
}
