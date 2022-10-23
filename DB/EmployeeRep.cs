namespace DB;

public class EmployeeRep
{
    //todo: сюда нужно передать конект из DbContext
    //todo: Все запросы к таблице employee. Все запросы из форм переносятся сюда
    public Employee GetItem(uint id)
    {
        return new Employee();
    }

    public List<Employee> GetItems(string login)
    {
        return new List<Employee>();
    }

    public void Save(Employee employee)
    {
        //todo: SQL команда на сохранение и ее выполнение
        if (employee.ID == 0)
        {
            Insert(employee);
        }
        else
        {
            Update(employee);
        }
    }

    private void Update(Employee employee)
    {

    }

    private void Insert(Employee employee)
    {

    }
}