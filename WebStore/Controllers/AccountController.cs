using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

    public class AccountController : Controller
    {
    public IActionResult Login() => View();
    public IActionResult Logout() => RedirectToAction("Index", "Home");
    public IActionResult Register() => View();
    public IActionResult AccessDenied() => View();


    }

