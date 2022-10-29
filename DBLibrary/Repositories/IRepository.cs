using MySqlConnector;
using System.Data;

namespace DBLibrary;

public interface IRepository<T>
{
    public T GetById(uint id);
    public List<T> GetAll();
    public void Create(T entity);
    public void Update(T entity);
    public T Record(IDataRecord record);
    public void Delete(uint id);    
}

