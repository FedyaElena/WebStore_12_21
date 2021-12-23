using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components;

public class BrandViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}