using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class IsCompletePublicHoliday
    {
        public bool Value { get; }

        private IsCompletePublicHoliday(bool value)
        {
            Value = value;
        }

        public static Result<IsCompletePublicHoliday> Create(bool isCompletePublicHoliday)
        {
            var result = new Result<IsCompletePublicHoliday>();

            result.WithValue(new IsCompletePublicHoliday(isCompletePublicHoliday));

            return result;
        }
    }
}
