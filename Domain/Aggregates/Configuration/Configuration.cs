using Domain.Aggregates.Configuration.ValueObjects;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.Configuration
{
    public class Configuration
    {
        public ConfigurationId Id { get; }
        public WorkingHoursPerDay WorkingHoursPerDay { get; private set; }
        public VacationDaysCount VacationDaysCount { get; private set; }
        public bool IsEnabled { get; private set; }
        public TogglTrackAccessToken TogglTrackAccessToken { get; private set; }

        private Configuration(ConfigurationId id, WorkingHoursPerDay workingHoursPerDay,
            VacationDaysCount vacationDaysCount, bool isEnabled, TogglTrackAccessToken togglTrackAccessToken)
        {
            Id = id;
            WorkingHoursPerDay = workingHoursPerDay;
            VacationDaysCount = vacationDaysCount;
            IsEnabled = isEnabled;
            TogglTrackAccessToken = togglTrackAccessToken;
        }

        public Result UpdateTogglTrackAccessToken(TogglTrackAccessToken togglTrackAccessToken)
        {
            var result = new Result();

            if (TogglTrackAccessToken.Value == togglTrackAccessToken.Value)
                return result;

            if (result.IsFailed)
                return result;

            TogglTrackAccessToken = togglTrackAccessToken;

            return result;
        }

        public Result UpdateWorkingHoursPerDay(WorkingHoursPerDay workingHoursPerDay)
        {
            var result = new Result();

            if (WorkingHoursPerDay.Value == workingHoursPerDay.Value)
                return result;

            if (result.IsFailed)
                return result;

            WorkingHoursPerDay = workingHoursPerDay;

            return result;
        }

        public Result UpdateVacationDaysCount(VacationDaysCount vacationDaysCount)
        {
            var result = new Result();

            if (VacationDaysCount.Value == vacationDaysCount.Value)
                return result;

            if (vacationDaysCount.Value < 0 || vacationDaysCount.Value > 365)
                result.WithError(new InvalidError(nameof(vacationDaysCount), vacationDaysCount));

            if (result.IsFailed)
                return result;

            VacationDaysCount = vacationDaysCount;
            return result;
        }

        public Result Enable()
        {
            var result = new Result();

            IsEnabled = true;

            return result;
        }

        public Result Disable()
        {
            var result = new Result();

            IsEnabled = false;

            return result;
        }
    }
}
