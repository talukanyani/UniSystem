namespace UniSystem.Core.Exceptions;

public sealed class DuplicateStudentException : Exception
{
    public DuplicateStudentException(string studentNumber)
        : base(
            $"A student with student number " +
            $"'{studentNumber}' already exists."
        )
    {
    }
}
