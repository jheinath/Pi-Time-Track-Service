using System;

namespace Domain.Aggregates.TimeSheet.Entities
{
    public class WorkingHours
    {
        public Guid Id { get; }
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset EndTime { get; }
        public TimeSpan TimeSpan()
        {
            return EndTime - StartTime;
        }
    }

    
}
