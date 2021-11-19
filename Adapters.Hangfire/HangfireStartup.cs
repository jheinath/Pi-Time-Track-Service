using Adapters.Hangfire.Interfaces;
using Hangfire;

namespace Adapters.Hangfire
{
    public class HangfireStartup : IHangfireStartup
    {
        private readonly IConfigurationStartupJob _configurationStartupJob;

        public HangfireStartup(IConfigurationStartupJob configurationStartupJob)
        {
            _configurationStartupJob = configurationStartupJob;
        }

        public void AddHangfireJobs()
        {
            BackgroundJob.Enqueue(() => _configurationStartupJob.Execute());
        }
    }
}
