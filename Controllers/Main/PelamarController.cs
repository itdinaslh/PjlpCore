using Microsoft.AspNetCore.Mvc;

namespace PjlpCore.Controllers.Main
{
    public class PelamarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
