using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using PjlpCore.Models;
using PjlpCore.Repository;
using System.Globalization;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin, Kepeg")]
public class PjlpController : Controller
{
    private readonly IPegawai pegRepo;
    private readonly IFilePegawai fileRepo;

    public PjlpController(IPegawai pRepo, IFilePegawai fRepo)
    {
        pegRepo = pRepo;
        fileRepo = fRepo;
    }

    [HttpGet("/pegawai/pjlp/details")]
    public async Task<IActionResult> Details(Guid bid, Guid pid)
    {
        Pegawai? peg = await pegRepo.Pegawais.Where(p => p.PegawaiID == pid)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .Include(p => p.Pendidikan)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .Include(x => x.DetailPjlp)
            .ThenInclude(x => x.Jabatan)
            .FirstOrDefaultAsync();

        List<FilePegawai>? filePegawai = await fileRepo.FilePegawais
            .Where(x => x.PegawaiID == pid)
            .Include(p => p.Persyaratan)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        string pasfoto = "";

        if (filePegawai is not null)
        {
            if (filePegawai.Any(x => x.PersyaratanID == 2))
            {
                #nullable disable
                pasfoto = filePegawai.Where(x => x.PersyaratanID == 2).Select(x => x.FilePath + "/" + x.FileName).FirstOrDefault();
                #nullable enable
            }
        }

        if (peg is not null)
        {
            string? lahir = peg.TglLahir.ToString();
            string? sim = "";
            string? myJab = null;

            if (peg.DetailPjlp is null)
            {
                peg.DetailPjlp = new DetailPjlp();                
            }

            sim = peg.DetailPjlp.MasaBerlakuSIM!.ToString();

            return View("~/Views/Main/Pegawai/PJLP/Details.cshtml", new PjlpVM
            {
                Pegawai = peg,
                NamaAgama = peg.Agama.NamaAgama,
                NamaBidang = peg.Bidang.NamaBidang,
                NamaPendidikan = peg.Pendidikan!.NamaPendidikan,
                TanggalLahir = DateTime.Parse(lahir!).ToString("dd-MM-yyyy"),
                Kelurahan = peg.KelurahanID is null ? "" : peg.Kelurahan!.NamaKelurahan,
                Kecamatan = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.NamaKecamatan,
                KecID = peg.KelurahanID is null ? "" : peg.Kelurahan!.KecamatanID,
                KabID = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.KabupatenID,
                Kabupaten = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.Kabupaten.NamaKabupaten,
                ProvID = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.Kabupaten.ProvinsiID,
                Provinsi = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
                KelurahanDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.NamaKelurahan,
                KecDomID = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.KecamatanID,
                KecamatanDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.NamaKecamatan,
                KabDomID = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.KabupatenID,
                KabupatenDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.Kabupaten.NamaKabupaten,
                ProvDomID = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.Kabupaten.ProvinsiID,
                ProvinsiDom = peg.KelurahanDomID is null ? "" : peg.KelurahanDom!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
                IsSame = peg.AddressIsSame,
                Files = filePegawai,
                PasFoto = pasfoto != "" ? pasfoto : null,
                MasaBerlakuSIM = sim,
                NamaJabatan = peg.DetailPjlp!.JabatanID is not null ? peg.DetailPjlp!.Jabatan!.NamaJabatan : null
            });
        }

        return NotFound();
    }

    [HttpPost("/pegawai/pjlp/biodata/update")]
    public async Task<IActionResult> UpdateBiodata(PjlpVM model)
    {
    #nullable disable
        if (ModelState.IsValid)
        {
            model.Pegawai.TglLahir = DateOnly.Parse(model.TanggalLahir, new CultureInfo("id-ID"));

            await pegRepo.UpdateBiodata(model.Pegawai);
            return Json(Result.Success());
        }

        return View("~/Views/Main/Pegawai/PJLP/Details.cshtml", model);
    }

    [HttpPost("/pegawai/pjlp/alamat/update")]
    public async Task<IActionResult> UpdateAlamat(PjlpVM model)
    {
        model.Pegawai.AddressIsSame = model.IsSame;

        if (model.Pegawai.PegawaiID != Guid.Empty)
        {
            if (model.IsSame)
            {
                model.Pegawai.AlamatDom = model.Pegawai.AlamatKTP;
                model.Pegawai.RtDom = model.Pegawai.RtKTP;
                model.Pegawai.RwDom = model.Pegawai.RwKTP;
                model.Pegawai.KodePosDom = model.Pegawai.KodePosKTP;
                model.Pegawai.KelurahanDomID = model.Pegawai.KelurahanID;
            }

            await pegRepo.UpdateAlamat(model.Pegawai);

            return Json(Result.Success());
        }

        return View("~/Views/Main/Pegawai/PJLP/Details.cshtml", model);
    }

    [HttpPost("/pegawai/pjlp/lainnya/update")]
    public async Task<IActionResult> UpdateLain(PjlpVM model)
    {
        if (model.Pegawai.PegawaiID != Guid.Empty)
        {
            if (model.MasaBerlakuSIM is not null)
            {
                model.Pegawai.DetailPjlp!.MasaBerlakuSIM = DateOnly.Parse(model.MasaBerlakuSIM, new CultureInfo("id-ID"));
            }

            await pegRepo.UpdateDataLain(model.Pegawai);

            return Json(Result.Success());
        }

        return View("~/Views/Main/Pegawai/PJLP/Details.cshtml", model);
    }

}
