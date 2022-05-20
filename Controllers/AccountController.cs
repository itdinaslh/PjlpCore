using Microsoft.AspNetCore.Mvc;

namespace PjlpCore.Controllers;

public class AccountController : Controller {
    [Route("/account/denied")]
    public IActionResult Denied() {
        return View();
    }
}