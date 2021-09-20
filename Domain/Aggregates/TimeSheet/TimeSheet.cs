using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates.TimeSheet.Entities;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet
{
    public class TimeSheet
    {
        public Guid Id { get; }
        public int Year { get; }
        public IEnumerable<TimeRecord> TimeRecords { get; }

        private TimeSheet(Guid id, int year, IEnumerable<TimeRecord> timeRecords)
        {
            Id = id;
            Year = year;
            TimeRecords = timeRecords;
        }

        public static Result<TimeSheet> Create(int year)
        {
            var result = new Result<TimeSheet>();

            if (year < 0 || year > 9999)
                result.Errors.Add(new InvalidError(nameof(year), year));

            if (result.IsFailed)
                return result;

            return result.WithValue(new TimeSheet(Guid.NewGuid(), year, new List<TimeRecord>()));
        }

        public Result AddTimeRecord(TimeRecord timeRecord)
        {
            var result = new Result();

            if (timeRecord == null)
                result.WithError(new RequiredError(nameof(timeRecord)));

            if (TimeRecords.Any(e => timeRecord != null && e.Day.Date == timeRecord.Day.Date))
                result.WithError("Time record already exists.");

            var newTotalCountRecords = TimeRecords.Count();
            if (newTotalCountRecords > DaysWithinYear)
                result.WithError("All time records are already entered");

            if (result.IsFailed)
                return result;

            TimeRecords.ToList().Add(timeRecord);

            return result;
        }

        public Result RemoveTimeRecord(int dayOfYear)
        {
            var result = new Result();

            if (dayOfYear > DaysWithinYear || dayOfYear < 0)
                result.WithError(new InvalidError(nameof(dayOfYear), dayOfYear));

            if (TimeRecords.Any(e => e.Day.DayOfYear == dayOfYear) == false)
                result.WithError("Time record does not exist.");

            if (result.IsFailed)
                return result;

            TimeRecords.ToList().RemoveAll(record => record.Day.DayOfYear == dayOfYear);

            return result;
        }

        private int DaysWithinYear => DateTime.IsLeapYear(Year) ? 366 : 365;
    }
}
