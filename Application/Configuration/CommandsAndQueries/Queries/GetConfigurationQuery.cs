using System.Threading.Tasks;
using Application.Configuration.Ports;
using FluentResults;

namespace Application.Configuration.CommandsAndQueries.Queries
{
    public class GetConfigurationQuery : IGetConfigurationQuery
    {
        private readonly IConfigurationRepository _configurationRepository;

        public GetConfigurationQuery(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<Result<Domain.Aggregates.Configuration.Configuration>> ExecuteAsync()
        {
            var configuration = await _configurationRepository.GetAsync();

            return Result.Ok(configuration);
        }
    }

    public interface IGetConfigurationQuery
    {
        Task<Result<Domain.Aggregates.Configuration.Configuration>> ExecuteAsync();
    }
}