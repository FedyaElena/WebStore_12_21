using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;
namespace WebStore.Components;

public class UserInfoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() => User.Identity?.IsAuthenticated == true
        ? View("UserInfo")
        : View();
}