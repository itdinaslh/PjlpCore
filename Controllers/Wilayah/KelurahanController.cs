using Microsoft.AspNetCore.Mvc;
using PjlpCore.Models.Wilayah;
using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

[Authorize]
public class KelurahanController : Controller {
    private IKelurahanRepo repo;

    public KelurahanController(IKelurahanRepo kelurahanRepo) {
        repo = kelurahanRepo;
    }

    [HttpGet("/wilayah/kelurahan")]
    public IActionResult Index() {
        return View("~/Views/Wilayah/Kelurahan/Index.cshtml");
    }

    [HttpGet("/wilayah/kelurahan/create")]
    public IActionResult Create() {
        return PartialView("~/Views/Wilayah/Kelurahan/AddEdit.cshtml", 
            new KelurahanViewModel { Kelurahan = new Kelurahan { KelurahanID = string.Empty }, IsNew = true });
    }
}