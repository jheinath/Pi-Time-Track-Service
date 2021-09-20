using System;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.CalendarYear.Entities
{
    public class PublicHoliday
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public DateTimeOffset DateTime { get; private set; }
        public bool IsHalfDayHoliday { get; private set; }

        private PublicHoliday(bool isHalfDayHoliday, DateTimeOffset dateTime, string name, Guid id)
        {
            IsHalfDayHoliday = isHalfDayHoliday;
            DateTime = dateTime;
            Name = name;
            Id = id;
        }

        public static Result<PublicHoliday> Create(bool isHalfDayHoliday, DateTimeOffset dateTime, string name)
        {
            var result = new Result<PublicHoliday>();

            if (name == null)
                result.Errors.Add(new RequiredError(nameof(Name)));

            if (result.IsFailed)
                return result;

            return result.WithValue(new PublicHoliday(isHalfDayHoliday, new DateTimeOffset(new DateTime(2020, dateTime.Month, dateTime.Day)), name, Guid.NewGuid()));
        }

        public Result<PublicHoliday> Update(bool isHalfDayHoliday, DateTimeOffset dateTime, string name)
        {
            var result = new Result<PublicHoliday>();

            if (name == null)
                result.Errors.Add(new RequiredError(nameof(Name)));

            if (result.IsFailed)
                return result;

            IsHalfDayHoliday = isHalfDayHoliday;
            DateTime = new DateTimeOffset(new DateTime(2020, dateTime.Month, dateTime.Day));
            Name = name;

            return result.WithValue(this);
        }
    }
}
