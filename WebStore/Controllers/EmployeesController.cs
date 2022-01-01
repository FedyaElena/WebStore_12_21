using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.ViewModels;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    //[Route("empl/[action]/{Id?}")]
    //[Route("Staff/[action]/{Id?}")]
    //[Authorize]
    public class EmployeesController : Controller
    {
        //private readonly ICollection<Employee> __Employees;
        private readonly IEmployeeData _EmployeesData;
        private readonly ILogger<EmployeesController> _Logger;
        public EmployeesController(IEmployeeData EmployeesData, ILogger<EmployeesController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        public IActionResult Index()
        {
            var result = _EmployeesData.GetEmployees();
            return View(result);
        }
        //[Route("~/employees/info-{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View("Edit", new EmployeeEditViewModel());
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new EmployeeEditViewModel());
            var employee = _EmployeesData.GetById((int)id);
            if (employee == null)
            {
                _Logger.LogWarning("При редактировании сотрудника с id:{0} он не был найден", id);
                return NotFound();
            }
               

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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel Model)
        {
            if (Model.LastName == "Асама" && Model.FirstName == "Бин" && Model.Patronymic == "Ладен")
                ModelState.AddModelError("", "Террористов на работу не берем!");

            if (!ModelState.IsValid)
                return View(Model);
            
           var employee = new Employee
            {
                Id = Model.Id,
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                Age = Model.Age,
                EmploymentDate = Model.EmploymentDate,
                Patronymic = Model.Patronymic,
            };

            if(Model.Id == 0)
            {
               
                _EmployeesData.Add(employee);
                _Logger.LogInformation("Создание нового сотрудника {0}", employee);
            }
               
            else if (!_EmployeesData.Edit(employee))
            {
                _Logger.LogInformation("Информация о сотруднике {0} изменена", employee);
                return NotFound();
            }
                

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_EmployeesData.Delete(id))
                return NotFound();

            _Logger.LogInformation("Сотрудник с id {0} удален", id);

            return RedirectToAction("Index");
        }



    }
}
