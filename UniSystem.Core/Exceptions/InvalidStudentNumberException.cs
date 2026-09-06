namespace UniSystem.Core.Exceptions;

public sealed class InvalidStudentNumberException
    : DomainValidationException
{
    public InvalidStudentNumberException(string? studentNumber)
        : base(
            $"'{studentNumber}' is not a valid student number. " +
            "A student number must contain exactly 8 digits."
        )
    {
    }
}
