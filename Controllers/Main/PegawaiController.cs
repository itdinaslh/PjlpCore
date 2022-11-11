using Microsoft.AspNetCore.Mvc;

namespace PjlpCore.Controllers;

public class PegawaiController : Controller
{
    [HttpGet("/pegawai/pjlp")]
    public IActionResult Pjlp()
    {
        return View("~/Views/Main/Pegawai/Pjlp.cshtml");
    }
}
