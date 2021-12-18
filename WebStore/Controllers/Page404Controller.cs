using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class Page404 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }
    }
}
