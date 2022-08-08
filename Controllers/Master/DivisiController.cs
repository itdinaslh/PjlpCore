using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models.Master;
using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class DivisiController : Controller {
    private IDivisiRepo repo;
    private IBidangRepo provRepo;

    public DivisiController(IDivisiRepo kRepo, IBidangRepo pRepo) {
        repo = kRepo; provRepo = pRepo;
    }

    [HttpGet("/master/divisi")]
    public IActionResult Index() {
        return View("~/Views/Master/Divisi/Index.cshtml");
    }

    [HttpGet("/master/divisi/create")]    
    public IActionResult Create() => PartialView("~/Views/Master/Divisi/AddEdit.cshtml", 
        new DivisiViewModel { Divisi = new Divisi { 
            DivisiID = Guid.Empty 
        }, IsNew = true });

    #nullable disable
    [HttpGet("/master/divisi/edit")]    
    public async Task<IActionResult> Edit(Guid divisiID) {
        Divisi div = await repo.Divisis.FirstOrDefaultAsync(k => k.DivisiID == divisiID);
        Bidang prov = await provRepo.Bidangs.FirstOrDefaultAsync(p => p.BidangID == div.BidangID);

        return PartialView("~/Views/WIlayah/Divisi/AddEdit.cshtml", new DivisiViewModel {
            Divisi = div,
            NamaBidang = prov.NamaBidang,
            IsNew = false,
            ExistingID = div.DivisiID
        });
    }

    [HttpPost("/master/divisi/save")]    
    public async Task<IActionResult> SaveDivisiAsync(DivisiViewModel div) {
        div.Divisi.DivisiID = Guid.NewGuid();
        if (ModelState.IsValid) {
            await repo.SaveDivisiAsync(div.Divisi);
            return Json(Result.Success());
        } else {
            return PartialView("~/Views/Master/Divisi/AddEdit.cshtml", div);
        }
    }
}