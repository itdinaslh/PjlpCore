﻿using Microsoft.AspNetCore.Mvc;
using PjlpCore.Repository;
using PjlpCore.Models;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.IO.Compression;
using System.Globalization;

namespace PjlpCore.Controllers;

[Authorize]
public class PelamarController : Controller
{
    private readonly IPersyaratanRepo pRepo;
    private readonly IEventFile eventRepo;
    private readonly IPelamar pelamarRepo;
    private readonly IFilePelamar fileRepo;
    private readonly IStatus statusRepo;

    public PelamarController(IPersyaratanRepo pRepo, IEventFile fRepo, IPelamar pelamarRepo, IFilePelamar fileLamar, IStatus statusRepo) { 
        this.pRepo = pRepo;
        this.eventRepo = fRepo;
        this.pelamarRepo = pelamarRepo;
        this.fileRepo = fileLamar;
        this.statusRepo = statusRepo;
    }

    [HttpGet("/pelamar/files")]
    [Authorize(Roles = "SysAdmin, PPBJ, Kepeg")]
    public async Task<IActionResult> FileWajib(Guid? jab, bool? isNew)
    {
        List<Persyaratan> dataList = new List<Persyaratan>();
        List<EventFile> events = new List<EventFile>();

        if (jab is not null && isNew is not null)
        {
            events = await eventRepo.EventFiles
                .Where(x => x.IsNew == isNew)
                .Where(x => x.JabatanID == jab)
                .ToListAsync();
        }

        dataList = await pRepo.Persyaratans.ToListAsync();

        double bagi = dataList.Count() / 2;

        int batas = bagi % 2 == 0 ? (int)bagi : (int)Math.Ceiling(bagi);

        List<SelectedList> syaratList = new List<SelectedList>();

        foreach (var data in dataList)
        {
            bool exist = events.Any(x => x.PersyaratanID == data.PersyaratanID);

            SelectedList list = new SelectedList
            {
                ID = data.PersyaratanID,
                Text = data.NamaPersyaratan,
                Selected = exist
            };

            syaratList.Add(list);
        }

        return View("~/Views/Pelamar/FileWajib/Index.cshtml", new FileWajibVM
        {
            ListSyarat = syaratList,
            Batas = batas
        });
    }

    [HttpPost("/pelamar/files/update")]
    [Authorize(Roles = "SysAdmin, PPBJ, Kepeg")]
    public async Task<IActionResult> UpdateFileWajib(FileWajibVM model, int[] Files)
    {
        model.Files = Files;

        if (ModelState.IsValid)
        {
            await eventRepo.SaveDataAsync(model);

            return Json(Result.Success());
        }        

        
        return Json(Result.Failed());
    }

    [HttpGet("/pelamar/lama")]
    [Authorize(Roles = "SysAdmin, PPBJ, PjlpAdmin, Kepeg")]
    public IActionResult Lama()
    {
        return View();
    }

    [HttpGet("/pelamar/baru")]
    [Authorize(Roles = "SysAdmin, PPBJ, PjlpAdmin, Kepeg")]
    public IActionResult Baru()
    {
        return View();
    }

    [HttpGet("/pelamar/details")]
    [Authorize(Roles = "SysAdmin, PPBJ, PjlpAdmin, Kepeg")]
    public async Task<IActionResult> Details(Guid bid, Guid pid)
    {
        Pelamar? data = await pelamarRepo.Pelamars
            .Where(x => x.PelamarId == pid)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .Include(p => p.Pendidikan)
            .Include(j => j.Jabatan)
            .Include(x => x.StatusLamaran)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .FirstOrDefaultAsync();        

        if (data is not null)
        {
            string? lahir = data.TglLahir.ToString();

            int HarusUpload = await eventRepo.EventFiles
                .Where(x => x.JabatanID == data.JabatanId)
                .Where(x => x.IsNew == data.IsNew)
                .CountAsync();            

            List<FilePelamar>? filePelamar = await fileRepo.FilePelamars
            .Where(x => x.PelamarId == pid)
            .Include(p => p.Persyaratan)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

            int SudahUpload = filePelamar is null ? 0 : filePelamar.Count();
            int BelumUpload = HarusUpload - SudahUpload;

            string pasfoto = "";

            if (filePelamar is not null)
            {
                if (filePelamar.Any(x => x.PersyaratanID == 2))
                {
#nullable disable
                    pasfoto = filePelamar.Where(x => x.PersyaratanID == 2).Select(x => x.FilePath + "/" + x.FileName).FirstOrDefault();
#nullable enable
                }
            }

            pasfoto = pasfoto.Contains("pdf") ? null : pasfoto;

            return View("~/Views/Pelamar/Details.cshtml", new PelamarVM
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
                PasFoto = pasfoto != "" ? pasfoto : null,
                Files = filePelamar,
                TotalUploaded = SudahUpload,
                TotalNotUploaded = BelumUpload < 0 ? 0 : BelumUpload
            });
        }

        return NotFound();
    }

    [HttpGet("/pelamar/files/download/single")]
    public async Task<IActionResult> DownloadSingle(Guid fileID)
    {
        var file = await fileRepo.FilePelamars.FirstOrDefaultAsync(x => x.FilePelamarID == fileID);

        var isPdf = file!.FileExtension.ToLower() == ".pdf";
        var mime = isPdf ? "application/pdf" : "image/png";

        var path = Uploads.Path + file.RealPath + "/" + file.FileName;

        return File(System.IO.File.ReadAllBytes(path), mime, System.IO.Path.GetFileName(path));
    }

    [HttpPost("/pelamar/files/download/selected")]
    [Authorize(Roles = "SysAdmin, PPBJ, PjlpAdmin, Kepeg")]
    public async Task DownloadSelected(Guid[] Files)
    {
        var files = await fileRepo.FilePelamars
            .Include(p => p.Pelamar)
            .Where(x => Files.Any(i => i == x.FilePelamarID))
            .ToListAsync();

        var myID = files.FirstOrDefault();

        Response.ContentType = "application/octet-stream";
        Response.Headers.Add("Content-Disposition", "attachment; filename=" + myID!.Pelamar.Nama + " - filtered.zip");

        using (ZipArchive archive = new ZipArchive(Response.BodyWriter.AsStream(), ZipArchiveMode.Create))
        {
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var entry = archive.CreateEntry(fileName);

                using var entryStream = entry.Open();
                using var fileStream = System.IO.File.OpenRead(Uploads.Path + "/uploads/pelamar/" + file.PelamarId.ToString() + "/" + fileName);

                await fileStream.CopyToAsync(entryStream);
            }
        }

    }

    [HttpPost("/pelamar/files/download/all")]
    [Authorize(Roles = "SysAdmin, PPBJ, PjlpAdmin, Kepeg")]
    public async Task DownloadAll(Guid myID)
    {
        var files = await fileRepo.FilePelamars
            .Include(p => p.Pelamar)
            .Where(x => x.PelamarId == myID).ToListAsync();

        var theID = files.FirstOrDefault();

        Response.ContentType = "application/octet-stream";
        Response.Headers.Add("Content-Disposition", "attachment; filename=" + theID!.Pelamar.Nama + ".zip");


        using (ZipArchive archive = new ZipArchive(Response.BodyWriter.AsStream(), ZipArchiveMode.Create))
        {
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var entry = archive.CreateEntry(fileName);

                using var entryStream = entry.Open();
                using var fileStream = System.IO.File.OpenRead(Uploads.Path + "/uploads/pelamar/" + file.PelamarId.ToString() + "/" + fileName);

                await fileStream.CopyToAsync(entryStream);
            }
        }
    }

    [HttpPost("/pelamar/status/change")]
    [Authorize(Roles = "SysAdmin, PPBJ")]
    public async Task<IActionResult> ChangeStatus(PelamarVM model)
    {
        if (model.Pelamar is not null)
        {
            var data = await pelamarRepo.Pelamars
            .FirstOrDefaultAsync(x => x.PelamarId == model.Pelamar.PelamarId);

            data!.StatusLamaranId = model.Pelamar.StatusLamaranId;

            Status? mystatus = await statusRepo.Statuses.FirstOrDefaultAsync(x => x.StatusId == data!.StatusLamaranId);

            await pelamarRepo.SaveDataAsync(data);

            return Json(Result.ChangeStatus(mystatus!.NamaStatus));
        }

        return Json(Result.Failed());
        
    }

    [HttpPost("/pelamar/jenis/pindah")]
    [Authorize(Roles = "SysAdmin")]
    public async Task<IActionResult> PindahinCoy(Guid? myID)
    {
        Pelamar? data = await pelamarRepo.Pelamars.FirstAsync(x => x.PelamarId == myID);

        if (data is not null)
        {
            await pelamarRepo.Pindahin(data);

            return Json(Result.Success());
        }

        return Json(Result.NotFound());
    }

    [HttpPost("/pelamar/biodata/update")]
    [Authorize(Roles = "SysAdmin, PjlpAdmin")]
    public async Task<IActionResult> UpdateBiodata(PelamarVM model)
    {
        if (model.Pelamar.PelamarId != Guid.Empty)
        {
            model.Pelamar.TglLahir = DateOnly.Parse(model.TanggalLahir!, new CultureInfo("id-ID"));

            await pelamarRepo.UpdateBiodata(model.Pelamar);

            return Json(Result.Success());
        }

        return Json(Result.Failed());
    }

    [HttpPost("/pelamar/alamat/update")]
    [Authorize(Roles = "SysAdmin, PjlpAdmin")]
    public async Task<IActionResult> UpdateAlamat(PelamarVM model)
    {
        model.Pelamar.AddressIsSame = model.AddressIsSame;

        if (model.Pelamar.PelamarId != Guid.Empty)
        {
            if (model.AddressIsSame)
            {
                model.Pelamar.DomKelurahanId = model.Pelamar.KelurahanId;
                model.Pelamar.DomAlamat = model.Pelamar.Alamat;
                model.Pelamar.DomRT = model.Pelamar.RT;
                model.Pelamar.DomRW = model.Pelamar.RW;
                model.Pelamar.DomKodePos = model.Pelamar.KodePos;
            }

            await pelamarRepo.UpdateAlamat(model.Pelamar);

            return Json(Result.Success());
        }

        return Json(Result.Failed());
    }

    [HttpPost("/pelamar/lainnya/update")]
    [Authorize(Roles = "SysAdmin, PjlpAdmin")]
    public async Task<IActionResult> UpdateLainnya(PelamarVM model)
    {
        if (model.Pelamar.PelamarId != Guid.Empty)
        {
            if (model.TglAkhirSIM is not null)
            {
                model.Pelamar.TglAkhirSIM = DateOnly.Parse(model.TglAkhirSIM, new CultureInfo("id_ID"));
            }

            await pelamarRepo.UpdateLainnya(model.Pelamar);

            return Json(Result.Success());
        }

        return Json(Result.Failed());
    }
}
