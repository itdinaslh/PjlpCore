using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models.Master;
using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class TupoksiController : Controller {
    private ITupoksiRepo tupRepo;
    private IJabatanRepo jabRepo;

    public TupoksiController(ITupoksiRepo kRepo, IJabatanRepo pRepo) {
        tupRepo = kRepo; jabRepo = pRepo;
    }

    [HttpGet("/master/tupoksi")]
    public IActionResult Index() {
        return View("~/Views/Master/Tupoksi/Index.cshtml");
    }

    [HttpGet("/master/tupoksi/create")]    
    public IActionResult Create() => PartialView("~/Views/Master/Tupoksi/AddEdit.cshtml", 
        new TupoksiViewModel { Tupoksi = new Tupoksi { 
            TupoksiID = Guid.NewGuid() 
        }, IsNew = true });

    #nullable disable
    [HttpGet("/master/tupoksi/edit")]    
    public async Task<IActionResult> Edit(Guid tupoksiID) {
        Tupoksi div = await tupRepo.Tupoksis.FirstOrDefaultAsync(k => k.TupoksiID == tupoksiID);
        Jabatan jab = await jabRepo.Jabatans.FirstOrDefaultAsync(p => p.JabatanID == div.JabatanID);

        return PartialView("~/Views/Master/Tupoksi/AddEdit.cshtml", new TupoksiViewModel {
            Tupoksi = div,
            NamaJabatan = jab.NamaJabatan,
            IsNew = false,
            ExistingID = div.TupoksiID
        });
    }

    [HttpPost("/master/tupoksi/save")]    
    public async Task<IActionResult> SaveTupoksiAsync(TupoksiViewModel div) {
        // div.Tupoksi.TupoksiID = Guid.NewGuid();
        if (ModelState.IsValid) {
            await tupRepo.SaveTupoksiAsync(div);
            return Json(Result.Success());
        } else {
            return PartialView("~/Views/Master/Tupoksi/AddEdit.cshtml", div);
        }
    }
}