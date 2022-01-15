using Microsoft.AspNetCore.Mvc;

using WebStore.ViewModels;
using WebStore.Services.Interfaces;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Controllers;

    public class HomeController : Controller
    {

        public IActionResult Index([FromServices]IProductData ProductData)
        {
        var products = ProductData.GetProducts()
            .OrderBy(p => p.Order)
            .Take(6)
            .ToView();
        ViewBag.Products = products;
           // return Content("Data from first controller");
           return View();
        }

        public void Throw(string Message) => throw new ApplicationException(Message);

    public IActionResult Error404() => View();
    
}
