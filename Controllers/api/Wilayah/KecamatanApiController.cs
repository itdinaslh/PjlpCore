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
public class KecamatanApiController : Controller {
    private readonly IKecamatanRepo repo;

    public KecamatanApiController(IKecamatanRepo kecamatanRepo) {
        repo = kecamatanRepo;
    }

    #nullable disable
    [HttpPost("/api/wilayah/kecamatan")]
    public async Task<IActionResult> KecamatanTable() {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Kecamatans.Select(k => new {
            kecamatanID = k.KecamatanID,
            namaKecamatan = k.NamaKecamatan,
            namaKabupaten = k.Kabupaten.NamaKabupaten,
            namaProvinsi = k.Kabupaten.Provinsi.NamaProvinsi,
            latitude = k.Latitude,
            longitude = k.Longitude
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue)) {
            init = init.Where(a => a.namaKecamatan.ToLower().Contains(searchValue.ToLower()) ||
                a.namaKabupaten.ToLower().Contains(searchValue.ToLower())            
            );
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result};
        
        return Ok(jsonData);
    }

#nullable enable

    [HttpGet("/api/wilayah/kecamatan/search")]
    public async Task<IActionResult> Search(string? term, string kab)
    {
        var data = await repo.Kecamatans
            .Where(k => k.KabupatenID == kab)
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.NamaKecamatan.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.KecamatanID,
                namaKecamatan = s.NamaKecamatan
            }).Take(10).ToListAsync();

        return Ok(data);
    }
}