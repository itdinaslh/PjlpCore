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

    public PelamarApiController(IPelamar pelamarRepo, IUser userRepo)
    {
        this.pelamarRepo = pelamarRepo;
        this.userRepo = userRepo;
    }

    [Authorize(Roles = "SysAdmin, PPBJ, Kepeg, PjlpAdmin")]
    [HttpPost("/api/pelamar/lama")]
    public async Task<IActionResult> PelamarLama()
    {
        bool isBidang = User.IsInRole("PjlpAdmin") || User.IsInRole("PPBJ");
        List<Guid> bidangs = new();
        List<Bidang> bids = new();

        if (isBidang)
        {
            bids = await userRepo.Users
                .Where(u => u.UserName == User.Identity!.Name)
                .SelectMany(u => u.Bidangs)
                .ToListAsync();

            foreach (Bidang bidang in bids)
            {
                bidangs.Add(bidang.BidangID);
            }
        }

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
              jabatan = k.Jabatan.NamaJabatan,
              bidang = k.Bidang.NamaBidang,
              status = k.StatusLamaran.NamaStatus
          });

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
}
