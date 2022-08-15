using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models.Master;
using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class JabatanController : Controller {
    private IJabatanRepo repo;
    private IBidangRepo jabRepo;

    public JabatanController(IJabatanRepo kRepo, IBidangRepo pRepo) {
        repo = kRepo; jabRepo = pRepo;
    }

    [HttpGet("/master/jabatan")]
    public IActionResult Index() {
        return View("~/Views/Master/Jabatan/Index.cshtml");
    }

    [HttpGet("/master/jabatan/create")]    
    public IActionResult Create() => PartialView("~/Views/Master/Jabatan/AddEdit.cshtml", 
        new JabatanViewModel { Jabatan = new Jabatan { 
            JabatanID = Guid.NewGuid() 
        }, IsNew = true });

    #nullable disable
    [HttpGet("/master/jabatan/edit")]    
    public async Task<IActionResult> Edit(Guid jabatanID) {
        Jabatan div = await repo.Jabatans.FirstOrDefaultAsync(k => k.JabatanID == jabatanID);
        Bidang jab = await jabRepo.Bidangs.FirstOrDefaultAsync(p => p.BidangID == div.BidangID);

        return PartialView("~/Views/Master/Jabatan/AddEdit.cshtml", new JabatanViewModel {
            Jabatan = div,
            NamaBidang = jab.NamaBidang,
            IsNew = false,
            ExistingID = div.JabatanID
        });
    }

    [HttpPost("/master/jabatan/save")]    
    public async Task<IActionResult> SaveJabatanAsync(JabatanViewModel div) {
        // div.Jabatan.JabatanID = Guid.NewGuid();
        if (ModelState.IsValid) {
            await repo.SaveJabatanAsync(div);
            return Json(Result.Success());
        } else {
            return PartialView("~/Views/Master/Jabatan/AddEdit.cshtml", div);
        }
    }
}