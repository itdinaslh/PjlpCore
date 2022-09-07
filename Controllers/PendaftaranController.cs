using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "PjlpUser")]
public class PendaftaranController : Controller
{
    [HttpGet("/pendaftaran/index")]    
    public IActionResult Index()
    {
        return View();
    }
}
