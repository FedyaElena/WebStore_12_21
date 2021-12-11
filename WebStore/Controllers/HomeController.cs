using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           // return Content("Data from first controller");
           return View();
        }

        public string ConfiguredAction(string id, string Value1)
        {
            return $"Hello World! {id} - {Value1}";
        }
    }
}
