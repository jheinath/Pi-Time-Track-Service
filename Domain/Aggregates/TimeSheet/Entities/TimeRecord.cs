using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates.TimeSheet.ValueObjects;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.Entities
{
    public class TimeRecord
    {
        public TimeRecordId Id { get; }
        public Day Day { get; }
        public IEnumerable<WorkingHours> WorkingHours { get; }
        public SickTimeDuration SickTimeDuration { get; }
        public IsCompletePublicHoliday IsCompletePublicHoliday { get; }
        public IsHalfDayPublicHoliday IsHalfDayPublicHoliday { get; }

        private static TimeSpan TotalDurationWorking(IEnumerable<WorkingHours> workingHours)
        {
            var workingHoursSum = workingHours.Sum(workingEntry => workingEntry.TimeSpan().TotalHours);
            return TimeSpan.FromHours(workingHoursSum);
        }
      
        private TimeRecord(TimeRecordId id, Day day, IEnumerable<WorkingHours> workingHours,
            SickTimeDuration sickTimeDuration, IsCompletePublicHoliday isCompletePublicHoliday,
            IsHalfDayPublicHoliday isHalfDayPublicHoliday)
        {
            Id = id;
            Day = day;
            WorkingHours = workingHours;
            SickTimeDuration = sickTimeDuration;
            IsCompletePublicHoliday = isCompletePublicHoliday;
            IsHalfDayPublicHoliday = isHalfDayPublicHoliday;
        }

        public Result<TimeRecord> Create(TimeRecordId timeRecordId, Day day, IEnumerable<WorkingHours> workingHours,
            SickTimeDuration sickTimeDuration, IsCompletePublicHoliday isCompletePublicHoliday,
            IsHalfDayPublicHoliday isHalfDayPublicHoliday)
        {
            var result = new Result<TimeRecord>();

            if (workingHours == null)
                result.Errors.Add(new RequiredError(nameof(workingHours)));

            var workingHoursList = (workingHours ?? new List<WorkingHours>()).ToList();
            if (workingHours != null && workingHoursList.Any(timeSpan =>
                    timeSpan.EndTime.Value.Date != day.Value.Date || timeSpan.StartTime.Value.Date != day.Value.Date))
                result.WithError(new InvalidError(nameof(workingHours), workingHours));

            var totalDurationWorking = TotalDurationWorking(workingHoursList);
            if (totalDurationWorking.TotalHours < 0 || totalDurationWorking.TotalHours > 24)
                result.WithError(new InvalidError(nameof(workingHours), workingHours));

            if (sickTimeDuration.Value.TotalHours < 0 || sickTimeDuration.Value.TotalHours > 24)
                result.WithError(new InvalidError(nameof(sickTimeDuration), sickTimeDuration));

            var timeSumWorkingAndSick = totalDurationWorking.TotalHours + sickTimeDuration.Value.TotalHours;
            if (timeSumWorkingAndSick > 24 || timeSumWorkingAndSick < 0)
                result.WithError("Sum of working hours and sick time is not valid for one day (24 h).");

            if (isCompletePublicHoliday.Value == isHalfDayPublicHoliday.Value)
                result.WithError(
                    "Time record can not be half public holiday and complete public holiday at the same time.");

            if (result.IsFailed)
                return result;

            return result.WithValue(new TimeRecord(timeRecordId, day, workingHoursList, sickTimeDuration,
                isCompletePublicHoliday, isHalfDayPublicHoliday));
        }
    }
}
