using Microsoft.AspNetCore.Mvc;

using WebStore.ViewModels;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers;

    public class HomeController : Controller
    {

        public IActionResult Index([FromServices]IProductData ProductData)
        {
        var products = ProductData.GetProducts()
            .OrderBy(p => p.Order)
            .Take(6)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
            });
        ViewBag.Products = products;
           // return Content("Data from first controller");
           return View();
        }

        public void Throw(string Message) => throw new ApplicationException(Message);

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
