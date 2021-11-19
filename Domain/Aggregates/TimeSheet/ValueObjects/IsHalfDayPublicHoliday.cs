using FluentResults;

namespace Domain.Aggregates.TimeSheet.ValueObjects
{
    public class IsHalfDayPublicHoliday
    {
        public bool Value { get; }

        private IsHalfDayPublicHoliday(bool value)
        {
            Value = value;
        }

        public static Result<IsHalfDayPublicHoliday> Create(bool isHalfPublicHoliday)
        {
            var result = new Result<IsHalfDayPublicHoliday>();

            result.WithValue(new IsHalfDayPublicHoliday(isHalfPublicHoliday));

            return result;
        }
    }
}
