namespace UniSystem.Core.Exceptions;

public sealed class LecturerNotFoundException : Exception
{
    public LecturerNotFoundException(string staffNumber)
        : base(
            $"Lecturer '{staffNumber}' was not found."
        )
    {
    }
}
