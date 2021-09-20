using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.Entities
{
    public class TimeRecord
    {
        public Guid Id { get; }
        public DateTimeOffset Day { get; }
        public IEnumerable<WorkingHours> WorkingHours { get; }
        public TimeSpan SickTimeDuration { get; }
        public bool IsCompletePublicHoliday { get; }
        public bool IsHalfDayPublicHoliday { get; }

        private static TimeSpan TotalDurationWorking(IEnumerable<WorkingHours> workingHours)
        {
            var workingHoursSum = workingHours.Sum(workingEntry => workingEntry.TimeSpan().TotalHours);
            return TimeSpan.FromHours(workingHoursSum);
        }
      

        private TimeRecord(Guid id, DateTimeOffset day, IEnumerable<WorkingHours> workingHours, TimeSpan sickTimeDuration, bool isCompletePublicHoliday, bool isHalfDayPublicHoliday)
        {
            Id = id;
            Day = day;
            WorkingHours = workingHours;
            SickTimeDuration = sickTimeDuration;
            IsCompletePublicHoliday = isCompletePublicHoliday;
            IsHalfDayPublicHoliday = isHalfDayPublicHoliday;
        }

        public Result<TimeRecord> Create(DateTimeOffset day, IEnumerable<WorkingHours> workingHours, TimeSpan sickTimeDuration, bool isCompletePublicHoliday, bool isHalfDayPublicHoliday)
        {
            var result = new Result<TimeRecord>();

            if (workingHours == null)
                result.Errors.Add(new RequiredError(nameof(workingHours)));

            var workingHoursList = (workingHours ?? new List<WorkingHours>()).ToList();
            if (workingHours != null && workingHoursList.Any(timeSpan =>
                    timeSpan.EndTime.Date != day.Date || timeSpan.StartTime.Date != day.Date))
                result.WithError(new InvalidError(nameof(workingHours), workingHours));

            var totalDurationWorking = TotalDurationWorking(workingHoursList);
            if (totalDurationWorking.TotalHours < 0 || totalDurationWorking.TotalHours > 24)
                result.WithError(new InvalidError(nameof(workingHours), workingHours));

            if (sickTimeDuration.TotalHours < 0 || sickTimeDuration.TotalHours > 24)
                result.WithError(new InvalidError(nameof(sickTimeDuration), sickTimeDuration));

            var timeSumWorkingAndSick = totalDurationWorking.TotalHours + sickTimeDuration.TotalHours;
            if (timeSumWorkingAndSick > 24 || timeSumWorkingAndSick < 0)
                result.WithError("Sum of working hours and sick time is not valid for one day (24 h).");

            if (isCompletePublicHoliday == isHalfDayPublicHoliday)
                result.WithError(
                    "Time record can not be half public holiday and complete public holiday at the same time.");

            if (result.IsFailed)
                return result;

            return result.WithValue(new TimeRecord(Guid.NewGuid(), day, workingHoursList, sickTimeDuration,
                isCompletePublicHoliday, isHalfDayPublicHoliday));
        }
    }
}
