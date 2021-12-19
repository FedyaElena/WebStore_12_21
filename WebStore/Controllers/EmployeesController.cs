using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;
using WebStore.ViewModels;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{Id?}")]
    //[Route("Staff/[action]/{Id?}")]
    public class EmployeesController : Controller
    {
        //private readonly ICollection<Employee> __Employees;
        private readonly IEmployeeData _EmployeesData;
        public EmployeesController(IEmployeeData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index()
        {
            var result = _EmployeesData.GetAll();
            return View(result);
        }
        //[Route("~/employees/info-{id}")]
        public IActionResult Details(int Id)
        {
            //ViewData["TestValue"] = 123;            
            //var employee = __Employees.FirstOrDefault(e => e.Id == Id);
            var employee = _EmployeesData.GetById(Id);

            if (employee == null)
                return NotFound();

            ViewBag.SelectedEmployee = employee;

            return View(employee);
        }

        //public IActionResult Create() => View();
        public IActionResult Edit(int id)
        {
            var employee = _EmployeesData.GetById(id);
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

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel Model)
        {
            var employee = new Employee
            {
                Id = Model.Id,
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                Age = Model.Age,
                EmploymentDate = Model.EmploymentDate,
                Patronymic = Model.Patronymic,
            };

            if(!_EmployeesData.Edit(employee))
                return NotFound();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if(id<0)
                return BadRequest();
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
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
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_EmployeesData.Delete(id))
                return NotFound();
            return RedirectToAction("Index");
        }



    }
}
