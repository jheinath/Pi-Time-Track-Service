using System;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class Timestamp
    {
        public DateTimeOffset Value { get; }
        public Timestamp(DateTimeOffset value)
        {
            Value = value;
        }

        public static Result<Timestamp> Create(DateTimeOffset day)
        {
            var result = new Result<Timestamp>();

            result.WithValue(new Timestamp(day));

            return result;
        }
    }
}
