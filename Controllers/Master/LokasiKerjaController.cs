using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Helpers;

namespace PjlpCore.Controllers;

public class LokasiKerjaController : Controller
{
    private readonly ILokasiKerja repo;

    public LokasiKerjaController(ILokasiKerja repo)
    {
        this.repo = repo;
    }

    [HttpGet("/master/lokasi-kerja")]
    public IActionResult Index()
    {
        return View("~/Views/Master/LokasiKerja/Index.cshtml");
    }

    [HttpGet("/master/lokasi-kerja/create")]
    public IActionResult Create() => PartialView("~/Views/Master/LokasiKerja/AddEdit.cshtml", new LokasiKerja());

    [HttpGet("/master/lokasi-kerja/edit")]
    public async Task<IActionResult> Edit(int locID) => PartialView("~/Views/Master/LokasiKerja/AddEdit.cshtml", 
        await repo.LokasiKerjas.FirstOrDefaultAsync(x => x.LokasiKerjaID == locID));

    [HttpPost("/master/lokasi-kerja/store")]
    public async Task<IActionResult> Store(LokasiKerja model)
    {
        if (ModelState.IsValid)
        {
            await repo.SaveDataAsync(model);

            return Json(Result.Success());
        }

        return PartialView("~/Views/Master/LokasiKerja/AddEdit.cshtml", model);
    }
}
