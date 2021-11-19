using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates.TimeSheet.Entities;
using Domain.Aggregates.TimeSheet.ValueObjects;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.TimeSheet
{
    public class TimeSheet
    {
        public TimeSheetId TimeSheetId { get; }
        public Year Year { get; }
        public IEnumerable<TimeRecord> TimeRecords { get; }

        private TimeSheet(TimeSheetId timeSheetId, Year year, IEnumerable<TimeRecord> timeRecords)
        {
            TimeSheetId = timeSheetId;
            Year = year;
            TimeRecords = timeRecords;
        }

        public static Result<TimeSheet> Create(TimeSheetId timeSheetId, Year year)
        {
            var result = new Result<TimeSheet>();

            return result.WithValue(new TimeSheet(timeSheetId, year, new List<TimeRecord>()));
        }

        public Result AddTimeRecord(TimeRecord timeRecord)
        {
            var result = new Result();

            if (timeRecord == null)
                result.WithError(new RequiredError(nameof(timeRecord)));

            if (TimeRecords.Any(e => timeRecord != null && e.Day.Value.Date == timeRecord.Day.Value.Date))
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

            if (TimeRecords.Any(e => e.Day.Value.DayOfYear == dayOfYear) == false)
                result.WithError("Time record does not exist.");

            if (result.IsFailed)
                return result;

            TimeRecords.ToList().RemoveAll(record => record.Day.Value.DayOfYear == dayOfYear);

            return result;
        }

        private int DaysWithinYear => DateTime.IsLeapYear(Year.Value) ? 366 : 365;
    }
}
