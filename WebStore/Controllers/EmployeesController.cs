using Microsoft.AspNetCore.Mvc;

using WebStore.Models;


namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee { Id = 1, LastName = "Ivanov", FirstName = "Ivan", Patronymic = "Ivanovich", Age = 23, EmploymentDate = new DateTime(2021, 12, 01) },
            new Employee { Id = 2, LastName = "Petrov", FirstName = "Petr", Patronymic = "Petrovich", Age = 24, EmploymentDate = new DateTime(2019, 07, 09) },
            new Employee { Id = 3, LastName = "Sidorov", FirstName = "Semen", Patronymic = "Semenovich", Age = 28, EmploymentDate = new DateTime(2020, 10, 11) },
        };

        public IActionResult Index()
        {
            var result = __Employees;
            return View(result);
        }
    }
}
