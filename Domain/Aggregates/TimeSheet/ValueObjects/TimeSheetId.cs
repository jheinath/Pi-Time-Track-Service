using System;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class TimeSheetId
    {
        public Guid Value { get; }

        private TimeSheetId(Guid id)
        {
            Value = id;
        }

        public static Result<TimeSheetId> Create(Guid id)
        {
            var result = new Result<TimeSheetId>();

            if (id == Guid.Empty)
                result.WithError(new EmptyError(nameof(id)));

            if (result.IsFailed)
                return result;

            var timeSheetId = new TimeSheetId(id);
            result.WithValue(timeSheetId);

            return result;
        }

        public static Result<TimeSheetId> CreateNew()
        {
            return Create(Guid.NewGuid());
        }
    }
}
