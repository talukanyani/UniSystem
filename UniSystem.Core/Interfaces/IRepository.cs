namespace UniSystem.Core.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    void Add(T entity);

    T? GetById(Guid id);

    IReadOnlyCollection<T> GetAll();

    void Update(T entity);

    bool Remove(Guid id);

    bool Exists(Guid id);
}
