using System;
using Domain.Aggregates.TimeSheet.ValueObjects;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.Entities
{
    public class WorkingHours
    {
        public WorkingHours(WorkingHoursId workingHoursId, Timestamp startTime, Timestamp endTime)
        {
            WorkingHoursId = workingHoursId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public WorkingHoursId WorkingHoursId { get; }
        public Timestamp StartTime { get; }
        public Timestamp EndTime { get; }
        public TimeSpan TimeSpan()
        {
            return EndTime.Value - StartTime.Value;
        }

        private Result<WorkingHours> Create(WorkingHoursId workingHoursId, Timestamp startDateTime, Timestamp endDateTime)
        {
            var result = new Result<WorkingHours>();

            if (startDateTime.Value.DayOfYear != endDateTime.Value.DayOfYear)
                result.WithError("Start date and end date need to be on same day");

            if (endDateTime.Value - startDateTime.Value < System.TimeSpan.Zero)
                result.WithError("Timespan can not be negative");

            if (result.IsFailed)
                return result;

            return result.WithValue(new WorkingHours(workingHoursId, startDateTime, endDateTime));
        }
    }

    
}
