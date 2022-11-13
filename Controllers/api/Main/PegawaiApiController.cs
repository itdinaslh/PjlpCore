using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PjlpCore.Repository;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace PjlpCore.Controllers.api;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SysAdmin")]
public class PegawaiApiController : ControllerBase
{
    private readonly IPegawai repo;

    public PegawaiApiController(IPegawai repo) { this.repo = repo; }

#nullable disable
    [HttpPost("/api/pegawai/pjlp")]
    public async Task<IActionResult> PjlpTable()
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
          .Where(p => p.JenisPegawaiID == 2)
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
                a.nik.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result };

        return Ok(jsonData);
    }
}
