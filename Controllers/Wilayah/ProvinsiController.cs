using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

public class ProvinsiController : Controller {
    private IProvinsiRepo repo;

    public ProvinsiController(IProvinsiRepo pRepo) => repo = pRepo;

    [HttpGet("/wilayah/provinsi")]
    public IActionResult Index() {
        return View("/Views/Wilayah/Provinsi/Index.cshtml");
    }
}