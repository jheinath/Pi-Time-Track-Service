
namespace Adapters.Database.Repositories.Dbos
{
    public class ConfigurationDbo
    {
        public string Id { get; set; }
        public double WorkingHoursPerDay { get; set; }
        public int VacationDaysCount { get; set; }
        public bool IsEnabled { get; set; }
        public string TogglTrackAccessToken { get; set; }
    }
}
