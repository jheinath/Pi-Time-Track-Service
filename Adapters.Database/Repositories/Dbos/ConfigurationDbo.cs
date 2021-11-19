
namespace Adapters.Database.Repositories.Dbos
{
    public class ConfigurationDbo
    {
        public string Id { get; set; }
        public decimal WorkingHoursPerDay { get; set; }
        public int VactionDaysCount { get; set; }
        public bool IsEnabled { get; set; }
        public string TogglTrackAccessToken { get; set; }
    }
}
