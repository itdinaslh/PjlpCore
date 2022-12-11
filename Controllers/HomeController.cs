using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Data;
using Microsoft.AspNetCore.Identity;
using PjlpCore.Repository;
using PjlpCore.Entity;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Linq;

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
            return View("~/Views/Home/Main3.cshtml");
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

            var lama = pelamarRepo.Pelamars
                .Include(x => x.Bidang)
                .Where(x => x.IsNew == false)
                .Select(p => new {
                    p.Bidang.NamaBidang
                }
            );

            var baru = pelamarRepo.Pelamars
                .Include(x => x.Bidang)
                .Where(x => x.IsNew == true)
                .Select(p => new {
                    p.Bidang.NamaBidang
                });

            var groupLama = await lama.GroupBy(x => x.NamaBidang)
                .Select(x => new {
                    x.Key,
                    Jumlah = x.Count()
                })
                .OrderBy(x => x.Key)
                .ToListAsync();

            var groupBaru = await baru.GroupBy(x => x.NamaBidang)
                .Select(x => new {
                    x.Key,
                    Jumlah = x.Count()
                })
                .OrderBy(x => x.Key)
                .ToListAsync();

            List<PelamarDashVM> listLama = new List<PelamarDashVM>();
            List<PelamarDashVM> listBaru = new List<PelamarDashVM>();

            foreach(var p in groupLama) {
                PelamarDashVM vm = new PelamarDashVM {
                    NamaBidang = p.Key,
                    JumlahPelamar = p.Jumlah
                };

                listLama.Add(vm);
            }

            foreach(var p in groupBaru) {
                PelamarDashVM vm = new PelamarDashVM {
                    NamaBidang = p.Key,
                    JumlahPelamar = p.Jumlah
                };

                listBaru.Add(vm);
            }

            return View(new DashboardVM
            {                
                CountBidang = bidRepo.Bidangs.Count(),                
                CountPelamar = pelamarRepo.Pelamars.Where(x => x.EventId == 1).Count(),
                CountBaru = pelamarRepo.Pelamars.Where(x => x.EventId == 1).Where(x => x.IsNew == true).Count(),
                CountLama = pelamarRepo.Pelamars.Where(x => x.EventId == 1).Where(x => x.IsNew == false).Count(),
                PelamarLama = listLama,
                PelamarBaru = listBaru
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
            string uri = Simpanan.AuthServer + HttpUtility.UrlEncode(Simpanan.ReturnUrl);

            return Redirect(uri);
        }
    }
}