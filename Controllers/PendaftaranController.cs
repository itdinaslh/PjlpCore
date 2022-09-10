using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Entity;
using System.Security.Claims;
using PjlpCore.Models.Main;

namespace PjlpCore.Controllers;

[Authorize(Roles = "PjlpUser")]
public class PendaftaranController : Controller
{
    [HttpGet("/pendaftaran/index")]    
    public IActionResult Index()
    {
        string? uid = ((ClaimsIdentity)User.Identity!).Claims.Where(c => c.Type == "sub").Select(c => c.Value).SingleOrDefault();
        string? email = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "email").Select(c => c.Value).SingleOrDefault();
        string? ktp = User.Identity.Name;
        string? nama = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "given_name").Select(c => c.Value).SingleOrDefault();

        return View(new PelamarVM
        {
            Pelamar = new Pelamar
            {
                UserId = Guid.Parse(uid!),
                UserEmail = email,
                NoKTP = ktp,
                Nama = nama
            }            
        });
    }
}
