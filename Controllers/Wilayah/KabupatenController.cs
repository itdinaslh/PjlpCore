using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models.Wilayah;
using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class KabupatenController : Controller {
    private IKabupatenRepo repo;
    private IProvinsiRepo provRepo;

    public KabupatenController(IKabupatenRepo kRepo, IProvinsiRepo pRepo) {
        repo = kRepo; provRepo = pRepo;
    }

    [HttpGet("/wilayah/kabupaten")]
    public IActionResult Index() {
        return View("~/Views/Wilayah/Kabupaten/Index.cshtml");
    }

    [HttpGet("/wilayah/kabupaten/create")]    
    public IActionResult Create() => PartialView("~/Views/Wilayah/Kabupaten/AddEdit.cshtml", 
        new KabupatenViewModel { Kabupaten = new Kabupaten { KabupatenID = string.Empty }, IsNew = true });

    #nullable disable
    [HttpGet("/wilayah/kabupaten/edit")]    
    public async Task<IActionResult> Edit(string kabupatenID) {
        Kabupaten kab = await repo.Kabupatens.FirstOrDefaultAsync(k => k.KabupatenID == kabupatenID);
        Provinsi prov = await provRepo.Provinsis.FirstOrDefaultAsync(p => p.ProvinsiID == kab.ProvinsiID);

        return PartialView("~/Views/WIlayah/Kabupaten/AddEdit.cshtml", new KabupatenViewModel {
            Kabupaten = kab,
            NamaProvinsi = prov.NamaProvinsi,
            IsNew = false,
            ExistingID = kab.KabupatenID
        });
    }

    [HttpPost("/wilayah/kabupaten/save")]    
    public async Task<IActionResult> SaveKabupatenAsync(KabupatenViewModel model) {
        if (ModelState.IsValid) {
            await repo.SaveKabupatenAsync(model);
            return Json(Result.Success());
        } else {
            return PartialView("~/Views/Wilayah/Kabupaten/AddEdit.cshtml", model);
        }
    }
}