using System.Threading.Tasks;
using FluentResults;

namespace Application.Configuration.CommandsAndQueries.Commands.Interfaces
{
    public interface IUpdateConfigurationCommand
    {
        Task<Result> ExecuteAsync(int vacationDays, decimal workingHoursPerDay, bool isEnabled, string accessToken);
    }
}