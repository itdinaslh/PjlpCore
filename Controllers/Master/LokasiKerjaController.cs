using Microsoft.AspNetCore.Mvc;

namespace PjlpCore.Controllers;

public class LokasiKerjaController : Controller
{
    [HttpGet("/master/lokasi-kerja")]
    public IActionResult Index()
    {
        return View();
    }
}
