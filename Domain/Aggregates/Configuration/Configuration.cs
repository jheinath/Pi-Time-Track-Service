using System;
using System.Collections.Generic;
using Domain.Aggregates.CalendarYear.Entities;

namespace Domain.Aggregates.Configuration
{
    public class Configuration
    {
        public Guid Id { get; }
        public IEnumerable<PublicHoliday> PublicHolidays { get; }
        public double WorkingHoursPerDay { get; }

    }
}
