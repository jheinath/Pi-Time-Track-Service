using System;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class Day
    {
        public DateTimeOffset Value { get; }
        public Day(DateTimeOffset value)
        {
            Value = value;
        }

        public static Result<Day> Create(DateTimeOffset day)
        {
            var result = new Result<Day>();
            
            result.WithValue(new Day(day));

            return result;
        }
    }
}
