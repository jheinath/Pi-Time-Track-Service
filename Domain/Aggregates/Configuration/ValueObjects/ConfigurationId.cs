using System;
using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.Configuration.ValueObjects
{
    public class ConfigurationId
    {
        public Guid Value { get; }

        private ConfigurationId(Guid value)
        {
            Value = value;
        }

        public static Result<ConfigurationId> Create(Guid timeRecordId)
        {
            var result = new Result<ConfigurationId>();

            if (timeRecordId == Guid.Empty)
                result.WithError(new EmptyError(nameof(timeRecordId)));

            if (result.IsFailed)
                return result;

            var configId = new ConfigurationId(timeRecordId);
            result.WithValue(configId);

            return result;
        }

        public static Result<ConfigurationId> CreateNew()
        {
            return Create(Guid.NewGuid());
        }
    }
}
