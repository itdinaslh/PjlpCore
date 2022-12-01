using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models.Master;
using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, Kepeg")]
public class JabatanController : Controller {
    private readonly IJabatanRepo repo;    

    public JabatanController(IJabatanRepo jRepo) {
        repo = jRepo;
    }

    [HttpGet("/master/jabatan")]
    public IActionResult Index() {
        return View("~/Views/Master/Jabatan/Index.cshtml");
    }

    [HttpGet("/master/jabatan/create")]    
    public IActionResult Create() => PartialView("~/Views/Master/Jabatan/AddEdit.cshtml", 
        new JabatanViewModel());

    #nullable disable
    [HttpGet("/master/jabatan/edit")]    
    public async Task<IActionResult> Edit(Guid jabatanID) {
        Jabatan div = await repo.Jabatans
            .Include(b => b.Bidang)
            .FirstOrDefaultAsync(k => k.JabatanID == jabatanID);        

        return PartialView("~/Views/Master/Jabatan/AddEdit.cshtml", new JabatanViewModel {
            Jabatan = div,
            NamaBidang = div.Bidang.NamaBidang
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