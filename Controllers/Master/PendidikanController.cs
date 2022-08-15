using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class PendidikanController : Controller {
    private IPendidikanRepo repo;

    public PendidikanController(IPendidikanRepo pendidikanRepo) {
        repo = pendidikanRepo;
    }

    [Route("/master/pendidikan")]
    public IActionResult Index() {
        return View("~/Views/Master/Pendidikan/Index.cshtml");
    }

    [HttpGet("/master/pendidikan/create")]
    public IActionResult Create() {
        return PartialView("~/Views/Master/Pendidikan/AddEdit.cshtml", new Pendidikan());
    }

    [HttpGet("/master/pendidikan/edit")]
    public async Task<IActionResult> Edit(int pendidikanId) {
        return PartialView("~/Views/Master/Pendidikan/AddEdit.cshtml", await repo.Pendidikans.FirstOrDefaultAsync(a => a.PendidikanID == pendidikanId));
    }

    [HttpPost("/master/pendidikan/save")]
    public async Task<IActionResult> SavePendidikanAsync(Pendidikan pendidikan) {
        if (ModelState.IsValid) {
            await repo.SavePendidikanAsync(pendidikan);
            var result = new Dictionary<string, bool>();
            result.Add("success", true);
            return Json(result);
        }

        return PartialView("~/Views/Master/Pendidikan/AddEdit.cshtml", pendidikan);
    }
}