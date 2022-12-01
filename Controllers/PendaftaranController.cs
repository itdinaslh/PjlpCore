using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Entity;
using System.Security.Claims;
using PjlpCore.Models;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Helpers;
using System.Globalization;
using System.Drawing;

namespace PjlpCore.Controllers;

[Authorize(Roles = "PjlpUser")]
public class PendaftaranController : Controller
{
    private readonly IPegawai repo;
    private readonly IPelamar pelamarRepo;

    public PendaftaranController(IPegawai repo, IPelamar pRepo)
    {
        this.repo = repo; 
        this.pelamarRepo = pRepo;
    }

    [HttpGet("/pendaftaran/index")]    
    public async Task<IActionResult> Index()
    {
        string? uid = ((ClaimsIdentity)User.Identity!).Claims.Where(c => c.Type == "sub").Select(c => c.Value).SingleOrDefault();
        string? email = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "email").Select(c => c.Value).SingleOrDefault();
        string? ktp = User.Identity.Name;
        string? nama = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "given_name").Select(c => c.Value).SingleOrDefault();

        Pegawai? peg = await repo.Pegawais
            .Where(x => x.NIK == ktp)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .Include(p => p.Pendidikan)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .Include(x => x.DetailPjlp)
            .FirstOrDefaultAsync();

        string? lahir = "";

        if (peg != null)
        {
            lahir = peg.TglLahir!.ToString();
        }

        if (peg!.DetailPjlp is null)
        {
            peg.DetailPjlp = new DetailPjlp();
        }

        return View(new PelamarVM
        {
            Pelamar = new Pelamar
            {
                UserId = Guid.Parse(uid!),
                UserEmail = email,
                NoKTP = ktp,
                Nama = nama,
                EventId = 1,
                Kelamin = peg!.Kelamin is null ? null : peg!.Kelamin!,
                GolonganDarah = peg!.GolDarah is null ? null : peg!.GolDarah!,
                Tanggungan = peg!.DetailPjlp!.Tanggungan is null ? null : peg!.DetailPjlp!.Tanggungan,
                IsNew = peg is null ? true : false,
            },
            isNew = peg is null ? true : false,
            TanggalLahir = DateTime.Parse(lahir!).ToString("dd-MM-yyyy"),
            NamaAgama = peg is null ? null : peg.Agama.NamaAgama,
            NamaPendidikan = peg!.PendidikanID is null ? null : peg.Pendidikan!.NamaPendidikan,
            Kelurahan = peg is null ? null : peg.Kelurahan!.NamaKelurahan,
            KecID = peg!.KelurahanID is null ? null : peg.Kelurahan!.KecamatanID,
            Kecamatan = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.NamaKecamatan,
            KabID = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.KabupatenID,
            Kabupaten = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.Kabupaten.NamaKabupaten,
            ProvID = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.Kabupaten.ProvinsiID,
            Provinsi = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
            NamaBidang = peg is null ? null : peg.Bidang.NamaBidang,
            Pegawai = peg is null ? new Pegawai() : peg
        });
    }

    [HttpPost("/pendaftaran/store")]
    public async Task<IActionResult> SavePendaftaran(PelamarVM model)
    {
        if (model.AddressIsSame)
        {
            model.Pelamar.DomKelurahanId = model.Pelamar.KelurahanId;
            model.Pelamar.DomAlamat = model.Pelamar.Alamat;
            model.Pelamar.DomRT = model.Pelamar.RT;
            model.Pelamar.DomRW = model.Pelamar.RW;
            model.Pelamar.DomKodePos = model.Pelamar.KodePos;
        }

        model.Pelamar.StatusLamaranId = 1;
        model.Pelamar.TglLahir = DateOnly.Parse(model.TanggalLahir!, new CultureInfo("id-ID"));

        if (ModelState.IsValid) {
            await pelamarRepo.SaveDataAsync(model.Pelamar);
            return Json(Result.Success());
        }

        return Json(Result.Failed());
    }

    [HttpGet("/pendaftaran/overview")]
    public async Task<IActionResult> Overview()
    {
        Pelamar? data = await pelamarRepo.Pelamars
            .Where(x => x.NoKTP == User.Identity!.Name)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .Include(p => p.Pendidikan)
            .Include(j => j.Jabatan)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .FirstOrDefaultAsync();

        if (data is not null)
        {
            string? lahir = data.TglLahir.ToString();

            return View("~/Views/Pendaftaran/Overview.cshtml", new PelamarVM
            {
                Pelamar = data,
                NamaAgama = data.Agama.NamaAgama,
                NamaBidang = data.Bidang.NamaBidang,
                NamaPendidikan = data.Pendidikan!.NamaPendidikan,
                TanggalLahir = DateTime.Parse(lahir!).ToString("dd-MM-yyyy"),
                Kelurahan = data.KelurahanId is null ? "" : data.Kelurahan!.NamaKelurahan,
                Kecamatan = data.KelurahanId is null ? "" : data.Kelurahan!.Kecamatan.NamaKecamatan,
                KecID = data.KelurahanId is null ? "" : data.Kelurahan!.KecamatanID,
                KabID = data.KelurahanId is null ? "" : data.Kelurahan!.Kecamatan.KabupatenID,
                Kabupaten = data.KelurahanId is null ? "" : data.Kelurahan!.Kecamatan.Kabupaten.NamaKabupaten,
                ProvID = data.KelurahanId is null ? "" : data.Kelurahan!.Kecamatan.Kabupaten.ProvinsiID,
                Provinsi = data.KelurahanId is null ? "" : data.Kelurahan!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
                KelurahanDom = data.DomKelurahanId is null ? "" : data.KelurahanDom!.NamaKelurahan,
                KecDomID = data.DomKelurahanId is null ? "" : data.KelurahanDom!.KecamatanID,
                KecamatanDom = data.DomKelurahanId is null ? "" : data.KelurahanDom!.Kecamatan.NamaKecamatan,
                KabDomID = data.DomKelurahanId is null ? "" : data.KelurahanDom!.Kecamatan.KabupatenID,
                KabupatenDom = data.DomKelurahanId is null ? "" : data.KelurahanDom!.Kecamatan.Kabupaten.NamaKabupaten,
                ProvDomID = data.DomKelurahanId is null ? "" : data.KelurahanDom!.Kecamatan.Kabupaten.ProvinsiID,
                ProvinsiDom = data.DomKelurahanId is null ? "" : data.KelurahanDom!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
            });
        }

        return NotFound();
    }
}
