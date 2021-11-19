using System.Threading.Tasks;

namespace Adapters.Hangfire.Interfaces
{
    public interface IConfigurationStartupJob
    {
        Task Execute();
    }
}
