using System;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class SickTimeDuration
    {
        public TimeSpan Value { get; }

        public SickTimeDuration(TimeSpan value)
        {
            Value = value;
        }

        public static Result<SickTimeDuration> Create(TimeSpan sickTimeDuration)
        {
            var result = new Result<SickTimeDuration>();

            if (sickTimeDuration < TimeSpan.Zero)
                result.WithError(new InvalidError(nameof(sickTimeDuration), sickTimeDuration));

            if (result.IsFailed)
                return result;

            result.WithValue(new SickTimeDuration(sickTimeDuration));

            return result;
        }
    }
}
