namespace UniSystem.Core.Exceptions;

public sealed class DuplicateDepartmentException : Exception
{
    public DuplicateDepartmentException(string code)
        : base(
            $"A department with code '{code}' already exists."
        )
    {
    }
}
