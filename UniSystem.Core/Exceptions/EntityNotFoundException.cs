namespace UniSystem.Core.Exceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityType, Guid id)
        : base($"{entityType} with ID '{id}' was not found.")
    {
    }
}
