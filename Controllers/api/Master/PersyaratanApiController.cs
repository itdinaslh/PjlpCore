using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers.api;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PersyaratanApiController : Controller {
    private IPersyaratanRepo repo;

    public PersyaratanApiController(IPersyaratanRepo persyaratanRepo) {
        repo = persyaratanRepo;
    }

    [HttpPost("/api/master/persyaratan")]
    public async Task<IActionResult> PersyaratanTable() {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Persyaratans;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue)) {
            init = init.Where(a => a.NamaPersyaratan.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result};
        
        return Ok(jsonData);
    }

    [HttpGet("/api/master/persyaratan/search")]
    public async Task<IActionResult> Search(string? term)
    {
        var data = await repo.Persyaratans
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.NamaPersyaratan.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.PersyaratanID,
                namaPersyaratan = s.NamaPersyaratan
            }).ToListAsync();

        return Ok(data);
    }
}