using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models.Wilayah;
using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class KecamatanController : Controller {
    private IKecamatanRepo repo;
    private IKabupatenRepo kabRepo;

    public KecamatanController(IKecamatanRepo kRepo, IKabupatenRepo kabupatenRepo) {
        repo = kRepo;
        kabRepo = kabupatenRepo;
    }

    [HttpGet("/wilayah/kecamatan")]
    public IActionResult Index() {
        return View("~/Views/Wilayah/Kecamatan/Index.cshtml");
    }

    [HttpGet("/wilayah/kecamatan/create")]
    public IActionResult Create() {
        return PartialView("~/Views/Wilayah/Kecamatan/AddEdit.cshtml", 
            new KecamatanViewModel { Kecamatan = new Kecamatan { KecamatanID = string.Empty }, IsNew = true });
    }

    #nullable disable
    [HttpGet("/wilayah/kecamatan/edit")]
    public async Task<IActionResult> Edit(string kecamatanID) {
        Kecamatan kec = await repo.Kecamatans.FirstOrDefaultAsync(k => k.KecamatanID == kecamatanID);
        Kabupaten kab = await kabRepo.Kabupatens.FirstOrDefaultAsync(k => k.KabupatenID == kec.KabupatenID);

        return PartialView("/Views/WIlayah/kecamatan/AddEdit.cshtml", new KecamatanViewModel {
            Kecamatan = kec,
            NamaKabupaten = kab.NamaKabupaten,
            IsNew = false,
            ExistingID = kec.KecamatanID
        });
    }

    [HttpPost("/wilayah/kecamatan/save")]    
    public async Task<IActionResult> SaveDataAsync(KecamatanViewModel model) {
        if (ModelState.IsValid) {
            await repo.SaveDataAsync(model);
            return Json(Result.Success());
        } else {
            return PartialView("/Views/Wilayah/Kecamatan/AddEdit.cshtml", model);
        }
    }
}