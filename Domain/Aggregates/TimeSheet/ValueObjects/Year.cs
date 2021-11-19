using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class Year
    {
        public int Value { get; }
        private Year(int value)
        {
            Value = value;
        }

        public static Result<Year> Create(int year)
        {
            var result = new Result<Year>();

            if (year < 0 || year > 9999)
                result.WithError(new InvalidError(nameof(year), year));

            if (result.IsFailed)
                return result;

            var yearObject = new Year(year);
            result.WithValue(yearObject);

            return result;
        }
    }
}
