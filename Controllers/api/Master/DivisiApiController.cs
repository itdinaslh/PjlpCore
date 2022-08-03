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
public class DivisiApiController : Controller {
    private IDivisiRepo repo;

    public DivisiApiController(IDivisiRepo kRepo) => repo = kRepo;

    #nullable disable
    [HttpPost("/api/master/divisi")]
    public async Task<IActionResult> DivisiTable() {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Divisis.Select(d => new {
            divisiID = d.DivisiID,
            namaDivisi = d.NamaDivisi,
            namaBidang = d.Bidang.NamaBidang
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue)) {
            init = init.Where(a => a.namaDivisi.ToLower().Contains(searchValue.ToLower()) ||
                a.namaBidang.ToLower().Contains(searchValue.ToLower())            
            );
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result};
        
        return Ok(jsonData);
    }

    [HttpGet("/api/master/kabupaten/search")]    
    public async Task<IActionResult> SearchDivisi(string term) {                      
        if (String.IsNullOrEmpty(term))
        {
            var bidangData = await repo.Divisis.
            Select(s => new
            {
                id = s.DivisiID,
                namaDivisi = s.NamaDivisi
            }).Take(5).ToListAsync();
            var data = bidangData;
            return Ok(data);
        } else
        {            
            var bid = await repo.Divisis.Where(p => p.NamaDivisi.ToLower().Contains(term.ToLower()))
                .Select(s => new {
                    id = s.DivisiID,
                    namaDivisi = s.NamaDivisi
                }).Take(5).ToListAsync();

            var data = bid;
            return Ok(data);
        }
    }
}