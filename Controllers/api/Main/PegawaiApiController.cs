using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PjlpCore.Repository;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using PjlpCore.Models;
using PjlpCore.Helpers;
using PjlpCore.Entity;
using System.Linq;

namespace PjlpCore.Controllers.api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PegawaiApiController : ControllerBase
{
    private readonly IPegawai repo;
    private readonly IUser userRepo;
    private readonly IBidangRepo bidangRepo;

    public PegawaiApiController(IPegawai repo, IUser userRepo, IBidangRepo bidangRepo)
    {
        this.repo = repo;
        this.userRepo = userRepo;
        this.bidangRepo = bidangRepo;
    }

#nullable disable

    [Authorize(Roles = "SysAdmin, PjlpAdmin, PPBJ")]
    [HttpPost("/api/pegawai/pjlp")]
    public async Task<IActionResult> PjlpTable()
    {
        bool isBidang = User.IsInRole("PjlpAdmin") || User.IsInRole("PPBJ");        
        List<Guid> bidangs = new();
        List<Bidang> bids = new();

        if (isBidang)
        {            
            bids = await userRepo.Users
                .Where(u => u.UserName == User.Identity.Name)
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

        var init = repo.Pegawais
          .Where(p => p.JenisPegawaiID == 2)
          .Where(p => isBidang ? bidangs.Contains(p.BidangID) : true)
          .Select(k => new {
            pegawaiID = k.PegawaiID,
            bidangID = k.BidangID,
            nik = k.NIK,
            namaPegawai = k.NamaPegawai,
            bidang = k.Bidang.NamaBidang,
            tglLahir = DateTime.Parse(k.TglLahir.ToString()).ToString("dd MMMM yyyy", new CultureInfo("id-ID")),
            noHP = k.NoHP
        });        

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init
                .Where(a => a.namaPegawai.ToLower()
                .Contains(searchValue.ToLower()) ||
                a.nik.ToLower().Contains(searchValue.ToLower()) ||
                a.bidang.ToLower().Contains(searchValue.ToLower())
           );
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [HttpPost("/api/pegawai/pns")]
    [Authorize(Roles = "SysAdmin, KepegDLH")]
    public async Task<IActionResult> PnsTable()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Pegawais
          .Where(p => p.JenisPegawaiID == 1)          
          .Select(k => new {
              pegawaiID = k.PegawaiID,
              namaPegawai = k.NamaPegawai,
              bidangID = k.BidangID,
              nik = k.NIK,
              nip = k.DetailAsn!.NIP,
              nrk = k.DetailAsn!.NRK,
              noHP = k.NoHP
          });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init
                .Where(a => a.namaPegawai.ToLower()
                .Contains(searchValue.ToLower()) ||
                a.nik.ToLower().Contains(searchValue.ToLower())
           );
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }

    [HttpGet("/api/pegawai/search")]    
    public async Task<IActionResult> SearchByNIK(string nik) {
        var peg = await repo.Pegawais
            .Where(p => p.NIK == nik)
            .Select(x => new {
                x.PegawaiID,
                x.NIK,
                x.NamaPegawai,
                x.NoHP,
                x.Email,
                x.AlamatKTP,
                x.Bidang.BidangID,
                x.Bidang.NamaBidang,
                x.TglLahir
            })
            .FirstOrDefaultAsync();

        if (peg is not null) {
            PegApiVM data = new PegApiVM {
                PegawaiID = peg.PegawaiID,
                NIK = peg.NIK,
                NamaPegawai = peg.NamaPegawai,
                NoHP = peg.NoHP,
                Email = peg.Email,
                AlamatKTP = peg.AlamatKTP,
                BidangID = peg.BidangID,
                NamaBidang = peg.NamaBidang,
                TglLahir = DateTime.Parse(peg.TglLahir.ToString()).ToString("dd-MM-yyyy")
            };

            return Ok(data);
        }

        return new JsonResult(Result.Failed());
    }
}
