using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, Kepeg")]
public class AgamaController : Controller {
    private IAgamaRepo repo;

    public AgamaController(IAgamaRepo agamaRepo) {
        repo = agamaRepo;
    }

    [Route("/master/agama")]
    public IActionResult Index() {
        return View("~/Views/Master/Agama/Index.cshtml");
    }

    [HttpGet("/master/agama/create")]
    public IActionResult Create() {
        return PartialView("~/Views/Master/Agama/AddEdit.cshtml", new Agama());
    }

    [HttpGet("/master/agama/edit")]
    public async Task<IActionResult> Edit(int agamaId) {
        return PartialView("~/Views/Master/Agama/AddEdit.cshtml", await repo.Agamas.FirstOrDefaultAsync(a => a.AgamaID == agamaId));
    }

    [HttpPost("/master/agama/save")]
    public async Task<IActionResult> SaveAgamaAsync(Agama agama) {
        if (ModelState.IsValid) {
            await repo.SaveAgamaAsync(agama);
            var result = new Dictionary<string, bool>();
            result.Add("success", true);
            return Json(result);
        }

        return PartialView("~/Views/Master/Agama/AddEdit.cshtml", agama);
    }
}