namespace DBLibrary.Interfaces;

public interface IRepository<T>
{
    public T? GetById(uint id);
    public List<T> GetAll();
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(uint id);
}

