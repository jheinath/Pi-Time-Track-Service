using System;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.Entities
{
    public class WorkingHours
    {
        public WorkingHours(Guid id, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; }
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset EndTime { get; }
        public TimeSpan TimeSpan()
        {
            return EndTime - StartTime;
        }

        private Result<WorkingHours> Create(DateTimeOffset startDateTimeOffset, DateTimeOffset endDateTimeOffset)
        {
            var result = new Result<WorkingHours>();

            if (startDateTimeOffset.DayOfYear != endDateTimeOffset.DayOfYear)
                result.WithError("Start date and end date need to be on same day");

            if (endDateTimeOffset - startDateTimeOffset < System.TimeSpan.Zero)
                result.WithError("Timespan can not be negative");

            if (result.IsFailed)
                return result;

            return result.WithValue(new WorkingHours(Guid.NewGuid(), startDateTimeOffset, endDateTimeOffset));
        }
    }

    
}
