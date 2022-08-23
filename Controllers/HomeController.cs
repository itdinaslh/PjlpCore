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
        private readonly IBidangRepo bidRepo;
        private readonly IPendidikanRepo penRepo;

        public HomeController(IJabatanRepo jRepo, IDivisiRepo dRepo, IBidangRepo bRepo, IPendidikanRepo pRepo) {
            jabRepo = jRepo; divRepo = dRepo; bidRepo=bRepo; penRepo=pRepo;
        }

        public IActionResult Index()
        {            
            return View(new DashboardVM {
                CountJabatan = jabRepo.Jabatans.Count(),
                CountDivisi = divRepo.Divisis.Count(),
                CountBidang = bidRepo.Bidangs.Count(),
                CountPendidikan = penRepo.Pendidikans.Count(),
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