using System;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class WorkingHoursId
    {
        public Guid Value { get; }

        private WorkingHoursId(Guid value)
        {
            Value = value;
        }

        public static Result<WorkingHoursId> Create(Guid timeRecordId)
        {
            var result = new Result<WorkingHoursId>();

            if (timeRecordId == Guid.Empty)
                result.WithError(new EmptyError(nameof(timeRecordId)));

            if (result.IsFailed)
                return result;

            var timeRecordIdValue = new WorkingHoursId(timeRecordId);
            result.WithValue(timeRecordIdValue);

            return result;
        }

        public static Result<WorkingHoursId> CreateNew()
        {
            return Create(Guid.NewGuid());
        }
    }
}
