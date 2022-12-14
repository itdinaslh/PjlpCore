using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Globalization;

namespace PjlpCore.Controllers.api;

[Route("api/[controller]")]
[ApiController]

public class PelamarApiController : ControllerBase
{
    private readonly IPelamar pelamarRepo;
    private readonly IUser userRepo;
    private readonly IUserBidang userBidangRepo;

    public PelamarApiController(IPelamar pelamarRepo, IUser userRepo, IUserBidang userBidangRepo)
    {
        this.pelamarRepo = pelamarRepo;
        this.userRepo = userRepo;
        this.userBidangRepo = userBidangRepo;
    }

    [Authorize(Roles = "SysAdmin, PPBJ, Kepeg, PjlpAdmin")]
    [HttpPost("/api/pelamar/lama")]
    public async Task<IActionResult> PelamarLama()
    {
        bool isBidang = User.IsInRole("PjlpAdmin") || User.IsInRole("PPBJ");
        List<Guid> bidangs = new();
        List<UserBidang> bids = new();

        if (isBidang)
        {
            var user = await userRepo.Users.Where(x => x.UserName == User.Identity!.Name).FirstOrDefaultAsync();

            bids = await userBidangRepo.UserBidangs
                .Where(x => x.UserID == user!.UserID)
                .ToListAsync();

            foreach (var p in bids)
            {
                bidangs.Add(p.BidangID);
            }
        }

        var bidang = Request.Form["bidang"].FirstOrDefault();
        var jabatan = Request.Form["jabatan"].FirstOrDefault();
        var status = Request.Form["status"].FirstOrDefault();        
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;        

        var init = pelamarRepo.Pelamars
          .Where(p => p.IsNew == false)
          .Where(p => isBidang ? bidangs.Contains(p.BidangId) : true)          
          .Select(k => new {
              pelamarId = k.PelamarId,
              bidangID = k.BidangId,
              noktp = k.NoKTP,
              nama = k.Nama,
              usia = GetAgeLastDec((DateOnly)k.TglLahir!) + " Tahun",
              umur = GetAgeLastDec((DateOnly)k.TglLahir),
              jabatanID = k.JabatanId,
              jabatan = k.Jabatan.NamaJabatan,
              bidang = k.Bidang.NamaBidang,
              statusID = k.StatusLamaranId,
              status = k.StatusLamaran.NamaStatus,
              isk2 = k.IsK2
          });

        init = bidang is not null ? init.Where(p => p.bidangID.ToString() == bidang) : init;
        init = jabatan is not null ? init.Where(p => p.jabatanID.ToString() == jabatan) : init;
        init = status is not null ? init.Where(p => p.statusID.ToString() == status) : init;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init
                .Where(a => a.nama.ToLower()
                .Contains(searchValue.ToLower()) ||
                a.noktp.ToLower().Contains(searchValue.ToLower()) ||
                a.bidang.ToLower().Contains(searchValue.ToLower())
           );
        }

        recordsTotal = init.Count();        

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [Authorize(Roles = "SysAdmin, PPBJ, Kepeg, PjlpAdmin")]
    [HttpPost("/api/pelamar/baru")]
    public async Task<IActionResult> PelamarBaru()
    {
        bool isBidang = User.IsInRole("PjlpAdmin") || User.IsInRole("PPBJ");
        List<Guid> bidangs = new();
        List<UserBidang> bids = new();

        if (isBidang)
        {
            var user = await userRepo.Users.Where(x => x.UserName == User.Identity!.Name).FirstOrDefaultAsync();

            bids = await userBidangRepo.UserBidangs
                .Where(x => x.UserID == user!.UserID)
                .ToListAsync();

            foreach (var p in bids)
            {
                bidangs.Add(p.BidangID);
            }
        }


        var bidang = Request.Form["bidang"].FirstOrDefault();
        var jabatan = Request.Form["jabatan"].FirstOrDefault();
        var status = Request.Form["status"].FirstOrDefault();
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = pelamarRepo.Pelamars
          .Where(p => p.IsNew == true)
          .Where(p => isBidang ? bidangs.Contains(p.BidangId) : true)
          .Select(k => new {
              pelamarId = k.PelamarId,
              bidangID = k.BidangId,
              noktp = k.NoKTP,
              nama = k.Nama,
              usia = GetAgeLastDec((DateOnly)k.TglLahir!) + " Tahun",
              umur = GetAgeLastDec((DateOnly)k.TglLahir),
              jabatanID = k.JabatanId,
              jabatan = k.Jabatan.NamaJabatan,
              bidang = k.Bidang.NamaBidang,
              statusID = k.StatusLamaranId,
              status = k.StatusLamaran.NamaStatus,
              isk2 = k.IsK2
          });

        init = bidang is not null ? init.Where(p => p.bidangID.ToString() == bidang) : init;
        init = jabatan is not null ? init.Where(p => p.jabatanID.ToString() == jabatan) : init;
        init = status is not null ? init.Where(p => p.statusID.ToString() == status) : init;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init
                .Where(a => a.nama.ToLower()
                .Contains(searchValue.ToLower()) ||
                a.noktp.ToLower().Contains(searchValue.ToLower()) ||
                a.bidang.ToLower().Contains(searchValue.ToLower())
           );
        }

        recordsTotal = init.Count();
        var result = await init.Skip(skip).Take(pageSize).ToListAsync();
        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    private static int GetAge(DateOnly birthDate)
    {
        DateTime n = DateTime.Now; // To avoid a race condition around midnight
        int age = n.Year - birthDate.Year;

        if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
            age--;

        return age;
    }

    private static int GetAgeLastDec(DateOnly birthDate)
    {
        DateTime n = new DateTime(2022, 12, 31);

        int age = n.Year - birthDate.Year;

        if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
            age--;

        return age;
    }
}
