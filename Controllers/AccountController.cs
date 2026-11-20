using Microsoft.AspNetCore.Mvc;
using RegistrationApp.ViewModels;

namespace RegistrationApp.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!string.IsNullOrWhiteSpace(model.Email) &&
            model.Email.Trim().EndsWith("@test.com", System.StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(nameof(model.Email), "test.com домені арқылы тіркелуге болмайды");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        TempData["RegisteredName"] = model.FullName.Trim();
        TempData["RegisteredEmail"] = model.Email.Trim();
        return RedirectToAction(nameof(Success));
    }

    [HttpGet]
    public IActionResult Success()
    {
        ViewBag.Email = TempData["RegisteredEmail"] as string;
        var name = TempData["RegisteredName"] as string ?? "Қолданушы";
        return View("Success", name);
    }
}
