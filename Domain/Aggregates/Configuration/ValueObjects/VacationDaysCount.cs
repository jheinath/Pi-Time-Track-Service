using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.Configuration.ValueObjects
{
    public class VacationDaysCount
    {
        public int Value { get; }
        private const int DefaultValue = 30;
        
        public VacationDaysCount(int value)
        {
            Value = value;
        }

        public static Result<VacationDaysCount> Create(int vacationDaysCount)
        {
            var result = new Result<VacationDaysCount>();

            if (vacationDaysCount < 0 || vacationDaysCount > 365)
                result.WithError(new InvalidError(nameof(vacationDaysCount), vacationDaysCount));

            if (result.IsFailed)
                return result;

            var timeRecordIdValue = new VacationDaysCount(vacationDaysCount);
            result.WithValue(timeRecordIdValue);

            return result;
        }

        public static Result<VacationDaysCount> CreateDefault()
        {
            return Create(DefaultValue);
        }
    }
}
