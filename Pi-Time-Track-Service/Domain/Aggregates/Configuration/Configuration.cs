using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Errors;
using FluentResults;
using Domain.Aggregates.Configuration.Entities;

namespace Domain.Aggregates.Configuration
{
    public class Configuration
    {
        public Guid Id { get; }
        public IEnumerable<PublicHoliday> PublicHolidays { get; }
        public double WorkingHoursPerDay { get; private set; }
        public int VacationDaysCount { get; private set; }
        public bool IsEnable { get; private set; }

        private Configuration(Guid id, IEnumerable<PublicHoliday> publicHolidays, double workingHoursPerDay, int vacationDaysCount, bool isEnable)
        {
            Id = id;
            PublicHolidays = publicHolidays;
            WorkingHoursPerDay = workingHoursPerDay;
            VacationDaysCount = vacationDaysCount;
            IsEnable = isEnable;
        }

        public static Configuration Create()
        {
            return new Configuration(Guid.NewGuid(), new List<PublicHoliday>(), 8.5, 30, false);
        }

        public Result AddPublicHoliday(PublicHoliday publicHoliday)
        {
            var result = new Result();

            if (publicHoliday == null)
                result.WithError(new RequiredError(nameof(publicHoliday)));

            //Day is already declared as holiday
            if (PublicHolidays.Any(holiday => publicHoliday != null && holiday.DateTime == publicHoliday.DateTime))
                result.WithError(new InvalidError(nameof(publicHoliday), publicHoliday));

            if (PublicHolidays.Count() > 300)
                result.WithError("Too many public holidays configured.");

            if (result.IsFailed)
                return result;

            PublicHolidays.ToList().Add(publicHoliday);

            return result;
        }

        public Result RemovePublicHoliday(DateTimeOffset dateTime)
        {
            var result = new Result();

            if (PublicHolidays.Any() == false)
                result.WithError("No public holidays to remove.");

            if (PublicHolidays.Any(holiday => holiday.DateTime == dateTime) == false)
                result.WithError("Item to remove does not exist.");

            if (result.IsFailed)
                return result;

            PublicHolidays.ToList().RemoveAll(holiday => holiday.DateTime == dateTime);

            return result;
        }

        public Result UpdateWorkingHoursPerDay(double workingHoursPerDay)
        {
            var result = new Result();

            if (workingHoursPerDay < 0 || workingHoursPerDay > 24)
                result.WithError(new InvalidError(nameof(workingHoursPerDay), workingHoursPerDay));

            if (result.IsFailed)
                return result;

            WorkingHoursPerDay = workingHoursPerDay;

            return result;
        }

        public Result UpdateVacationDaysCount(int vacationDaysCount)
        {
            var result = new Result();

            if (vacationDaysCount < 0 || vacationDaysCount > 365)
                result.WithError(new InvalidError(nameof(vacationDaysCount), vacationDaysCount));

            if (result.IsFailed)
                return result;

            VacationDaysCount = vacationDaysCount;

            return result;
        }

        public Result Enable()
        {
            var result = new Result();

            IsEnable = true;

            return result;
        }

        public Result Disable()
        {
            var result = new Result();

            IsEnable = false;

            return result;
        }
    }
}
