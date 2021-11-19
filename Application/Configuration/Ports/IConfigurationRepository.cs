using System.Threading.Tasks;

namespace Application.Configuration.Ports
{
    public interface IConfigurationRepository
    {
        Task InsertAsync(Domain.Aggregates.Configuration.Configuration configuration);
        Task UpdateAsync(Domain.Aggregates.Configuration.Configuration configuration);
        Task<Domain.Aggregates.Configuration.Configuration> GetAsync();
    }
}
