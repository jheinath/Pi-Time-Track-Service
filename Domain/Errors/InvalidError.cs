using FluentResults;

namespace Domain.Errors
{
    public class RequiredError : Error
    {
        public string FieldName { get; }

        public RequiredError(string fieldName)
            : base($"{fieldName} is required.")
        {
            FieldName = fieldName;
            Metadata.Add("ErrorCode", 100);
        }
    }
}
