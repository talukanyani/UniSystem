namespace UniSystem.Core.Exceptions;

public sealed class DuplicateLecturerException : Exception
{
    public DuplicateLecturerException(string staffNumber)
        : base(
            $"A lecturer with staff number " +
            $"'{staffNumber}' already exists."
        )
    {
    }
}
