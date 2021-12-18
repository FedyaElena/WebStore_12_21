using Microsoft.AspNetCore.Mvc;


namespace WebStore.Controllers;

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
           // return Content("Data from first controller");
           return View();
        }

     

        //public IActionResult Employees()
        //{
        //    return View(__Employees);
        //}

        //public IActionResult EmployeeCard(string id)
        //{
        //   foreach (var employee in __Employees)
        //    {
        //        if (employee.Id.ToString() == id)
        //        {
        //            return View(employee);
        //        }
      
        //    }
        //    return NotFound();

        //}
    
}
