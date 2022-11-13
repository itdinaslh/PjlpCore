using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Models;
using PjlpCore.Repository;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class PegawaiController : Controller
{
    private readonly IPegawai pegRepo;

    public PegawaiController(IPegawai pRepo)
    {
        pegRepo = pRepo;
    }

    [HttpGet("/pegawai/pjlp")]
    public IActionResult Pjlp()
    {
        return View("~/Views/Main/Pegawai/PJLP/Index.cshtml");
    }

    [HttpGet("/pegawai/pjlp/details")]
    public async Task<IActionResult> Details(Guid bid, Guid pid)
    {
        Pegawai? peg = await pegRepo.Pegawais.Where(p => p.PegawaiID == pid)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .FirstOrDefaultAsync();
        if (peg is not null)
        {
            return View("~/Views/Main/Pegawai/PJLP/Details.cshtml", new PegawaiVM
            {
                Pegawai = peg,
                NamaAgama = peg.Agama.NamaAgama,
                NamaBidang = peg.Bidang.NamaBidang
            });
        }

        return NotFound();
    }
}
