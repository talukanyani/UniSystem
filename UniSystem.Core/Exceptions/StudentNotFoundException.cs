namespace UniSystem.Core.Exceptions;

public sealed class StudentNotFoundException : Exception
{
    public StudentNotFoundException(string studentNumber)
        : base(
            $"Student '{studentNumber}' was not found."
        )
    {
    }
}
