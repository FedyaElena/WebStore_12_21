using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee { Id = 1, LastName = "Ivanov", FirstName = "Ivan", Patronymic = "Ivanovich", Age = 23 },
            new Employee { Id = 2, LastName = "Petrov", FirstName = "Petr", Patronymic = "Petrovich", Age = 24 },
            new Employee { Id = 3, LastName = "Sidorov", FirstName = "Semen", Patronymic = "Semenovich", Age = 28 },
        };
        public IActionResult Index()
        {
           // return Content("Data from first controller");
           return View();
        }

        public string ConfiguredAction(string id, string Value1)
        {
            return $"Hello World! {id} - {Value1}";
        }

        public IActionResult Employees()
        {
            return View(__Employees);
        }

        public IActionResult EmployeeCard(string id)
        {
           foreach (var employee in __Employees)
            {
                if (employee.Id.ToString() == id)
                {
                    return View(employee);
                }
      
            }
            return View(1);

        }
    }
}
