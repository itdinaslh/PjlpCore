using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Data;
using Microsoft.AspNetCore.Identity;
using PjlpCore.Repository;
using PjlpCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJabatanRepo jabRepo;
        private readonly IDivisiRepo divRepo;
        private readonly IBidangRepo bidRepo;
        private readonly IPendidikanRepo penRepo;
        private readonly IPelamar pelamarRepo;

        public HomeController(IJabatanRepo jRepo, IDivisiRepo dRepo, IBidangRepo bRepo, IPendidikanRepo pRepo, IPelamar pelRepo) {
            jabRepo = jRepo; divRepo = dRepo; bidRepo=bRepo; penRepo=pRepo; pelamarRepo = pelRepo;
        }

        [AllowAnonymous]
        [HttpGet("/")]
        public IActionResult Index()
        {            
            return View();
        }
        
        [HttpGet("/dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            if (User.IsInRole("PjlpUser"))
            {
                Pelamar? pelamar = await pelamarRepo.Pelamars.Where(p => p.NoKTP == User.Identity!.Name).FirstOrDefaultAsync();

                if (pelamar is null)
                {
                    return RedirectToAction("Index", "Pendaftaran");
                }
            }

            return View(new DashboardVM
            {
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