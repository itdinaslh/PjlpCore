using Microsoft.AspNetCore.Mvc;

namespace PjlpCore.Controllers;

public class PelamarController : Controller
{
    [HttpGet("/pelamar/baru")]
    public IActionResult Baru()
    {
        return View();
    }

    [HttpGet("/pelamar/lama")]
    public IActionResult Lama()
    {
        return View();
    }
}
