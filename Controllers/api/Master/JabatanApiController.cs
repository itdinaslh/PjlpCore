using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Models.Master;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers.api;

[ApiController]
[Route("[controller]")]
public class JabatanApiController : Controller {
    private IJabatanRepo repo;

    public JabatanApiController(IJabatanRepo kRepo) => repo = kRepo;

    #nullable disable
    [HttpPost("/api/master/jabatan")]
    public async Task<IActionResult> JabatanTable() {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Jabatans.Select(d => new {
            jabatanID = d.JabatanID,
            namaJabatan = d.NamaJabatan,
            namaBidang = d.Bidang.NamaBidang
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue)) {
            init = init.Where(a => a.namaJabatan.ToLower().Contains(searchValue.ToLower()) ||
                a.namaBidang.ToLower().Contains(searchValue.ToLower())            
            );
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result};
        
        return Ok(jsonData);
    }

#nullable enable

    [HttpGet("/api/master/jabatan/search")]
    public async Task<IActionResult> Search(string? term, Guid? bidang)
    {
        var data = await repo.Jabatans
            .Where(b => b.BidangID == bidang)
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.NamaJabatan.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.JabatanID,
                namaJabatan = s.NamaJabatan
            }).Take(10).ToListAsync();

        return Ok(data);
    }
}