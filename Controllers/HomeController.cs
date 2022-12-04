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
    public class HomeController : Controller
    {
        private readonly IJabatanRepo jabRepo;
        private readonly IDivisiRepo divRepo;
        private readonly IBidangRepo bidRepo;
        private readonly IPegawai pegRepo;
        private readonly IPelamar pelamarRepo;

        public HomeController(IJabatanRepo jRepo, IDivisiRepo dRepo, IBidangRepo bRepo, IPegawai pRepo, IPelamar pelRepo) {
            jabRepo = jRepo; divRepo = dRepo; bidRepo=bRepo; pegRepo = pRepo; pelamarRepo = pelRepo;
        }

        [AllowAnonymous]
        [HttpGet("/")]
        public IActionResult Index()
        {            
            return View("~/Views/Home/Main.cshtml");
        }
        
        [HttpGet("/dashboard")]
        [Authorize(Roles = "SysAdmin, PjlpUser, PjlpAdmin, Kepeg, PPBJ")]
        public async Task<IActionResult> Dashboard()
        {
            if (User.IsInRole("PjlpUser"))
            {
                Pelamar? pelamar = await pelamarRepo.Pelamars.Where(p => p.NoKTP == User.Identity!.Name).FirstOrDefaultAsync();

                if (pelamar is null)
                {
                    return RedirectToAction("Index", "Pendaftaran");
                } else
                {
                    return RedirectToAction("Overview", "Pendaftaran");
                }
            }

            return View(new DashboardVM
            {
                CountPNS = pegRepo.Pegawais.Where(x => x.JenisPegawaiID == 1).Count(),
                CountDivisi = divRepo.Divisis.Count(),
                CountBidang = bidRepo.Bidangs.Count(),
                CountPJLP = pegRepo.Pegawais.Where(x => x.JenisPegawaiID == 2).Count(),
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

        [HttpGet("/daftar")]
        public IActionResult Daftar()
        {
            Uri uri = new Uri(Simpanan.AuthServer + Simpanan.ReturnUrl);

            return Redirect(uri.ToString());
        }
    }
}