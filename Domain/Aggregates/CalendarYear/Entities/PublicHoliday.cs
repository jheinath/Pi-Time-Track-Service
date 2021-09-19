using System;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.CalendarYear.Entities
{
    public class PublicHoliday
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public bool IsHalfDayHoliday { get; private set; }

        private PublicHoliday(bool isHalfDayHoliday, DateTimeOffset date, string name, Guid id)
        {
            IsHalfDayHoliday = isHalfDayHoliday;
            Date = date;
            Name = name;
            Id = id;
        }

        public static Result<PublicHoliday> Create(bool isHalfDayHoliday, DateTimeOffset date, string name)
        {
            var result = new Result<PublicHoliday>();

            if (name == null)
                result.Errors.Add(new RequiredError(nameof(Name)));

            if (result.IsFailed)
                return result;

            return result.WithValue(new PublicHoliday(isHalfDayHoliday, date, name, Guid.NewGuid()));
        }

        public Result<PublicHoliday> Update(bool isHalfDayHoliday, DateTimeOffset date, string name)
        {
            var result = new Result<PublicHoliday>();

            if (name == null)
                result.Errors.Add(new RequiredError(nameof(Name)));

            if (result.IsFailed)
                return result;

            IsHalfDayHoliday = isHalfDayHoliday;
            Date = date;
            Name = name;

            return result.WithValue(this);
        }
    }
}
