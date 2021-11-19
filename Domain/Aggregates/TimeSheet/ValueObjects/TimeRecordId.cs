using System;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class TimeRecordId
    {
        public Guid Value { get; }

        private TimeRecordId(Guid value)
        {
            Value = value;
        }

        public static Result<TimeRecordId> Create(Guid timeRecordId)
        {
            var result = new Result<TimeRecordId>();

            if (timeRecordId == Guid.Empty)
                result.WithError(new EmptyError(nameof(timeRecordId)));

            if (result.IsFailed)
                return result;

            var timeRecordIdValue = new TimeRecordId(timeRecordId);
            result.WithValue(timeRecordIdValue);

            return result;
        }
    }
}
