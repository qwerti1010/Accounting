namespace DBLibrary.Interfaces;

public interface IRepository<T>
{
    public T? GetByID(uint id);
    public List<T> GetAll(int take, int skip);
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(uint id);
}

