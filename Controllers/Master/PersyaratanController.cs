using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, Kepeg, PjlpAdmin")]
public class PersyaratanController : Controller {
    private IPersyaratanRepo repo;

    public PersyaratanController(IPersyaratanRepo persyaratanRepo) {
        repo = persyaratanRepo;
    }

    [Route("/master/persyaratan")]
    public IActionResult Index() {
        return View("~/Views/Master/Persyaratan/Index.cshtml");
    }

    [HttpGet("/master/persyaratan/create")]
    public IActionResult Create() {
        return PartialView("~/Views/Master/Persyaratan/AddEdit.cshtml", new Persyaratan());
    }

    [HttpGet("/master/persyaratan/edit")]
    public async Task<IActionResult> Edit(int persyaratanId) {
        return PartialView("~/Views/Master/Persyaratan/AddEdit.cshtml", await repo.Persyaratans.FirstOrDefaultAsync(a => a.PersyaratanID == persyaratanId));
    }

    [HttpPost("/master/persyaratan/save")]
    public async Task<IActionResult> SavePersyaratanAsync(Persyaratan persyaratan) {
        if (ModelState.IsValid) {
            await repo.SavePersyaratanAsync(persyaratan);
            var result = new Dictionary<string, bool>();
            result.Add("success", true);
            return Json(result);
        }

        return PartialView("~/Views/Master/Persyaratan/AddEdit.cshtml", persyaratan);
    }
}