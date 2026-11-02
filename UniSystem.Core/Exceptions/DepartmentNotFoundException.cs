namespace UniSystem.Core.Exceptions;

public sealed class DepartmentNotFoundException : Exception
{
    public DepartmentNotFoundException(string code)
        : base($"Department '{code}' was not found.")
    {
    }

    public DepartmentNotFoundException(Guid id)
        : base($"Department with ID '{id}' was not found.")
    {
    }
}
