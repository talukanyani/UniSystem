namespace UniSystem.Core.Exceptions;

public class InvalidCourseCodeException : DomainValidationException
{
    public InvalidCourseCodeException(string courseCode) 
        : base(
            $"{courseCode} is not a valid course code. "
            + "Use three letters followed by three digits, for example CSC101."
            )
    {
    }
}
