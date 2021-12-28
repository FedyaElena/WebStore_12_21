//using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Data;
using WebStore.Domain;
using WebStore.DAL.Context;

namespace WebStore.Services.InSQL;

public class SQLEmployeesData : IEmployeeData
 {
    private readonly WebStoreDB _db;
    private readonly ILogger<SQLEmployeesData> _Logger;
    
    public SQLEmployeesData(WebStoreDB db) => _db = db;

    public IEnumerable<Employee> GetEmployees() => _db.Employees;

    public Employee? GetById(int id) => _db.Employees.FirstOrDefault(employee => employee.Id == id);

    public int Add(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        if (_db.Employees.Contains(employee))
            return employee.Id;
       
        _db.Employees.Add(employee);
        return employee.Id;
    }

    public bool Edit(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));
        if (_db.Employees.Contains(employee))
            return true;
        var db_employee = GetById(employee.Id);
        if (db_employee == null)
        {
            _Logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id:{0}", employee.Id);
            return false;
        }

        db_employee.FirstName = employee.FirstName;
        db_employee.LastName = employee.LastName;
        db_employee.Age = employee.Age;
        db_employee.Patronymic = employee.Patronymic;
        db_employee.EmploymentDate = employee.EmploymentDate;

        _Logger.LogInformation("Информация о сотруднике с Id:{0} была изменена", employee.Id);

        return true;
    }


    public bool Delete(int id)
    {
        var employee = GetById(id);
        if (employee == null)
        {
            _Logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id:{0}", id);
            return false;
        }

        _db.Employees.Remove(employee);
        //_Logger.LogInformation("Cотрудник с Id:{0} был удален", id);
        return true;
    }
}

