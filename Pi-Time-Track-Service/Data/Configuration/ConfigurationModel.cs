namespace Pi_Time_Track_Service.Data.Configuration
{
    public class ConfigurationModel
    {
        public string Id { get; set; }
        public decimal WorkingHoursPerDay { get; set; }
        public int VacationDaysCount { get; set; }
        public bool IsEnabled { get; set; }
        public string TogglTrackAccessToken { get; set; }
    }
}
