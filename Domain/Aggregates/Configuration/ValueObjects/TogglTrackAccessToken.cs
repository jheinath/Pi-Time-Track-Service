using Domain.Errors;
using FluentResults;

namespace Domain.Aggregates.Configuration.ValueObjects
{
    public class TogglTrackAccessToken
    {
        public TogglTrackAccessToken(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public Result<TogglTrackAccessToken> Create(string togglTrackAccessToken)
        {
            var result = new Result<TogglTrackAccessToken>();

            if (string.IsNullOrWhiteSpace(togglTrackAccessToken))
                result.WithError(new InvalidError(nameof(togglTrackAccessToken), togglTrackAccessToken));

            if (result.IsFailed)
                return result;

            var token = new TogglTrackAccessToken(togglTrackAccessToken);
            result.WithValue(token);

            return result;
        }
    }
}
