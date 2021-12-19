using WebStore.Models;
namespace WebStore.Data;

public static class TestData
{
   public static List<Employee> Employees { get; } = new()
       {
           new Employee { Id = 1, LastName = "Ivanov", FirstName = "Ivan", Patronymic = "Ivanovich", Age = 23, EmploymentDate = new DateTime(2021, 12, 01) },
           new Employee { Id = 2, LastName = "Petrov", FirstName = "Petr", Patronymic = "Petrovich", Age = 24, EmploymentDate = new DateTime(2019, 07, 09) },
           new Employee { Id = 3, LastName = "Sidorov", FirstName = "Semen", Patronymic = "Semenovich", Age = 28, EmploymentDate = new DateTime(2020, 10, 11) },
       };

}
