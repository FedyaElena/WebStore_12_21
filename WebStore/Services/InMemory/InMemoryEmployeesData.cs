//using WebStore.Models;
//using WebStore.Services.Interfaces;
//using WebStore.Data;

//namespace WebStore.Services.InMemory;
//public class InMemoryEmployeesData : IEmployeeData
//{
//    private readonly ILogger<InMemoryEmployeesData> _Logger;
//    private readonly ICollection<Employee> _Employees;
//    private int _MaxFreeId;
    
//    public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> Logger)
//    {
//        _Employees = TestData.Employees;
//        _MaxFreeId = _Employees.DefaultIfEmpty().Max(e => e?.Id ?? 0)+1;
//        _Logger = Logger;
//    }

//    public IEnumerable<Employee> GetAll() => _Employees;
    

//    public Employee? GetById(int id) => _Employees.FirstOrDefault(employee => employee.Id == id);
  

//    public int Add(Employee employee)
//    {
//       if (employee is null)
//            throw new ArgumentNullException(nameof(employee));

//        if (_Employees.Contains(employee))
//            return employee.Id;
//        employee.Id = _MaxFreeId++;
//        _Employees.Add(employee);
//        return employee.Id;
//    }

//    public bool Edit(Employee employee)
//    {
//        if (employee == null)
//            throw new ArgumentNullException(nameof(employee));
//        if (_Employees.Contains(employee))
//            return true;
//        var db_employee = GetById(employee.Id);
//        if (db_employee == null)
//        {
//            _Logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id:{0}", employee.Id);
//            return false;
//        }
            
//        db_employee.FirstName = employee.FirstName;
//        db_employee.LastName = employee.LastName;
//        db_employee.Age = employee.Age;
//        db_employee.Patronymic = employee.Patronymic;
//        db_employee.EmploymentDate = employee.EmploymentDate;

//        _Logger.LogInformation("Информация о сотруднике с Id:{0} была изменена", employee.Id);
        
//        return true;
//    }

//    public bool Delete(int id)
//    {
//        var employee = GetById(id);
//        if (employee == null)
//        {
//            _Logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id:{0}", id);
//            return false;
//        }
            
//        _Employees.Remove(employee);
//        _Logger.LogInformation("Cотрудник с Id:{0} был удален", id);
//        return true;
//    }


//}

