using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{Id?}")]
    //[Route("Staff/[action]/{Id?}")]
    public class EmployeesController : Controller
    {
        private readonly ICollection<Employee> __Employees;
        public EmployeesController()
        {
            __Employees = TestData.Employees;
        }

        public IActionResult Index()
        {
            var result = __Employees;
            return View(result);
        }
        //[Route("~/employees/info-{id}")]
        public IActionResult Details(int Id)
        {
            ViewData["TestValue"] = 123;            
            var employee = __Employees.FirstOrDefault(e => e.Id == Id);

            if (employee == null)
                return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

        //public IActionResult Create() => View();
        public IActionResult Edit(int id)
        {
            var employee = __Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            var model = new EmployeeEditViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                EmploymentDate = employee.EmploymentDate,
            };

            return View(model);
        }

        public IActionResult Edit(EmployeeEditViewModel Model)
        {
            //обработка модели

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) => View();



    }
}
