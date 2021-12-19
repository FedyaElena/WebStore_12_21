using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IEmployeeData
    {
        IEnumerable<Employee> GetAll();
        Employee? GetEmployee(int id);

        int Add(Employee employee);
        bool Edit(Employee employee);

        bool Delete(int id);
    }
}
