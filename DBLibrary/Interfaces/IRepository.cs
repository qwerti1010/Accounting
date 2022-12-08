namespace DBLibrary.Interfaces;

public interface IRepository<T>
{
    public T? GetByID(uint id);
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(uint id);
    public int Count();
}

