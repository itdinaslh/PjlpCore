using Microsoft.AspNetCore.Mvc;

namespace PjlpCore.Controllers.Main
{
    public class PnsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
