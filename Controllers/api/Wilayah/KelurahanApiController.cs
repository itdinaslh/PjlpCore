using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers.api;

[ApiController]
[Route("[controller]")]
public class KelurahanApiController : Controller {
    private IKelurahanRepo repo;

    public KelurahanApiController(IKelurahanRepo kelurahanRepo) {
        repo = kelurahanRepo;
    }

    #nullable disable
    [HttpPost("/api/wilayah/kelurahan")]
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

        var init = repo.Kelurahans.Select(k => new {
            kelurahanID = k.KelurahanID,
            namaKelurahan = k.NamaKelurahan,
            namaKecamatan = k.Kecamatan.NamaKecamatan,
            namaKabupaten = k.Kecamatan.Kabupaten.NamaKabupaten,
            namaProvinsi = k.Kecamatan.Kabupaten.Provinsi.NamaProvinsi            
        });

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue)) {
            init = init.Where(a => a.namaKelurahan.ToLower().Contains(searchValue.ToLower()) ||
                a.namaKecamatan.ToLower().Contains(searchValue.ToLower()) ||
                a.namaKabupaten.ToLower().Contains(searchValue.ToLower()) || 
                a.namaProvinsi.ToLower().Contains(searchValue.ToLower())
            );
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result};
        
        return Ok(jsonData);
    }
}