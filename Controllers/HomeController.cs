using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Data;
using Microsoft.AspNetCore.Identity;
using PjlpCore.Repository;

namespace PjlpCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJabatanRepo jabRepo;
        private readonly IDivisiRepo divRepo;

        public HomeController(IJabatanRepo jRepo, IDivisiRepo dRepo) {
            jabRepo = jRepo; divRepo = dRepo;
        }

        public IActionResult Index()
        {            
            return View(new DashboardVM {
                CountJabatan = jabRepo.Jabatans.Count(),
                CountDivisi = divRepo.Divisis.Count()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}