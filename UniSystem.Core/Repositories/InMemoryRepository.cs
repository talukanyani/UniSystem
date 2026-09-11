using UniSystem.Core.Exceptions;
using UniSystem.Core.Interfaces;

namespace UniSystem.Infrastructure.Repositories;

public sealed class InMemoryRepository<T> : IRepository<T>
    where T : class, IEntity
{
    private readonly Dictionary<Guid, T> _entities = new();

    public void Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        bool wasAdded = _entities.TryAdd(
            entity.Id,
            entity
        );

        if (!wasAdded)
        {
            throw new DuplicateEntityException(
                typeof(T).Name,
                entity.Id
            );
        }
    }

    public T? GetById(Guid id)
    {
        _entities.TryGetValue(id, out T? entity);
        return entity;
    }

    public IReadOnlyCollection<T> GetAll()
    {
        return _entities.Values.ToList().AsReadOnly();
    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (!_entities.ContainsKey(entity.Id))
        {
            throw new EntityNotFoundException(
                typeof(T).Name,
                entity.Id
            );
        }

        _entities[entity.Id] = entity;
    }

    public bool Remove(Guid id)
    {
        return _entities.Remove(id);
    }

    public bool Exists(Guid id)
    {
        return _entities.ContainsKey(id);
    }
}
