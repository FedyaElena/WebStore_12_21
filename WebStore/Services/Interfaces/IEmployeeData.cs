using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces
{
    public interface IEmployeeData
    {
        IEnumerable<Employee> GetEmployees();
        Employee? GetById(int id);

        int Add(Employee employee);
        bool Edit(Employee employee);

        bool Delete(int id);
    }
}
