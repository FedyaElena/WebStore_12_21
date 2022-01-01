using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels.Identity;
using WebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Controllers;

    public class AccountController : Controller
    {
    private readonly UserManager<User> _UserManager;
    private readonly SignInManager<User> _SignInManager;

    public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
    {
        _UserManager = UserManager;
        _SignInManager = SignInManager;
    }
    public IActionResult Register() => View(new RegisterUserViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserViewModel Model)
    {
        if (!ModelState.IsValid)
          return View(Model);

        var user = new User 
        {
            UserName = Model.UserName,
        };
        
        var registration_result = await _UserManager.CreateAsync(user, Model.Password);
        if (registration_result.Succeeded)
        {
            await _SignInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in registration_result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(Model);
    }
    public IActionResult Logout() => RedirectToAction("Index", "Home");
    public IActionResult Login(string ReturnUrl) => View(new LoginViewModel {ReturnUrl = ReturnUrl});

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel Model)
    {
        if (!ModelState.IsValid)
            return View(Model);

        var login_result = await _SignInManager.PasswordSignInAsync(
            Model.UserName,
            Model.Password,
            Model.RememberMe,
            true
            );
        if (login_result.Succeeded)
        {
            //if (Url.IsLocalUrl(Model.ReturnUrl))
            //    return Redirect(Model.ReturnUrl);
            //return RedirectToAction("Index", "Home");

            return LocalRedirect(Model.ReturnUrl ?? "/");
        }
        ModelState.AddModelError("", "Неверное имя пользователя или пароль");
        return View(Model);
    }

    public IActionResult AccessDenied() => View();


    }

