namespace UniSystem.Core.Exceptions;

public sealed class DuplicateEntityException : Exception
{
    public DuplicateEntityException(string entityType, Guid id)
        : base($"{entityType} with ID '{id}' already exists.")
    {
    }
}
