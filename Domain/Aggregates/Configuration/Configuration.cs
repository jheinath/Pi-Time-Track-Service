using System;
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

        public static Configuration Load(Guid id, decimal workingHoursPerDay,
            int vacationDaysCount, bool isEnabled, string togglTrackAccessToken)
        {
            return new Configuration(ConfigurationId.Load(id), WorkingHoursPerDay.Load(workingHoursPerDay),
                VacationDaysCount.Load(vacationDaysCount), isEnabled,
                TogglTrackAccessToken.Load(togglTrackAccessToken));
        }

        public static Result<Configuration> Create(ConfigurationId id, WorkingHoursPerDay workingHoursPerDay,
            VacationDaysCount vacationDaysCount, bool isEnabled, TogglTrackAccessToken togglTrackAccessToken)
        {
            return new Result<Configuration>().WithValue(new Configuration(id, workingHoursPerDay, vacationDaysCount, isEnabled, togglTrackAccessToken));
        }


        public static Result<Configuration> CreateNew()
        {
            var configurationId = ConfigurationId.CreateNew();
            var workingHours = WorkingHoursPerDay.CreateDefault();
            var vacationDayCount = VacationDaysCount.CreateDefault();

            var mergedResult = Result.Merge(configurationId, workingHours, vacationDayCount);

            if (mergedResult.IsFailed)
                return mergedResult;

            return Create(configurationId.Value, workingHours.Value, vacationDayCount.Value, false, null);
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
