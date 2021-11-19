using FluentResults;

namespace Domain.Errors
{
    public class EmptyError : Error
    {
        public string FieldName { get; }

        public EmptyError(string fieldName)
            : base($"{fieldName} is empty.")
        {
            FieldName = fieldName;
            Metadata.Add("ErrorCode", 100);
        }
    }
}
