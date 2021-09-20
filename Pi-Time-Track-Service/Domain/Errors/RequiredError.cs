using FluentResults;

namespace Domain.Errors
{
    public class InvalidError : Error
    {
        public string FieldName { get; }
        public object InvalidValue { get; }

        public InvalidError(string fieldName, object invalidValue)
            : base($"{fieldName} with value {invalidValue} is invalid.")
        {
            FieldName = fieldName;
            InvalidValue = invalidValue;
            Metadata.Add("ErrorCode", 101);
        }
    }
}
