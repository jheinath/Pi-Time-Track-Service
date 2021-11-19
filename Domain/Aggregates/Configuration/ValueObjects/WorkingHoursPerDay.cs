using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.Configuration.ValueObjects
{
    public class WorkingHoursPerDay
    {
        public decimal Value { get; }

        private WorkingHoursPerDay(decimal value)
        {
            Value = value;
        }

        public static Result<WorkingHoursPerDay> Create(decimal workingHoursPerDay)
        {
            var result = new Result<WorkingHoursPerDay>();

            if (workingHoursPerDay <= 0 || workingHoursPerDay > 24)
                result.WithError(new InvalidError(nameof(workingHoursPerDay), workingHoursPerDay));

            if (result.IsFailed)
                return result;

            var workingHoursPerDayValue = new WorkingHoursPerDay(workingHoursPerDay);
            result.WithValue(workingHoursPerDayValue);

            return result;
        }
    }
}
