using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PjlpCore.Repository;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class LokasiKerjaApiController : ControllerBase
{
    private readonly ILokasiKerja repo;

    public LokasiKerjaApiController(ILokasiKerja repo)
    {
        this.repo = repo;
    }

    [HttpPost("/api/master/lokasi-kerja")]
    public async Task<IActionResult> AgamaTable()
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

        var init = repo.LokasiKerjas;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue))
        {
            init = init.Where(a => a.NamaLokasi.ToLower().Contains(searchValue.ToLower()));
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = result };

        return Ok(jsonData);
    }
}
