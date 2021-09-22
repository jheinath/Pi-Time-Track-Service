using Domain.Aggregates.Configuration;
using System.Threading.Tasks;

namespace Adapters.Database.Repositories.ConfigurationRepository
{
    public interface IConfigurationRepository
    {
        Task InsertAsync(Configuration configuration);
    }
}
