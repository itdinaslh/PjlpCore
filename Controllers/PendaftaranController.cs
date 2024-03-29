﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Entity;
using System.Security.Claims;
using PjlpCore.Models;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Helpers;
using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PjlpCore.Controllers;

[Authorize]
public class PendaftaranController : Controller
{
    private readonly IPegawai repo;
    private readonly IPelamar pelamarRepo;
    private readonly IPegawai pegRepo;
    private readonly IFilePelamar fileRepo;
    private readonly IEventFile eventFileRepo;
    private readonly IEvent eventRepo;

    public PendaftaranController(IPegawai repo, IPelamar pRepo, IFilePelamar fRepo, IPegawai pegRepo, IEventFile eventFile, IEvent eventRepo)
    {
        this.repo = repo; 
        this.pelamarRepo = pRepo;
        this.fileRepo = fRepo;
        this.eventFileRepo = eventFile;
        this.pegRepo = pegRepo;
        this.eventRepo = eventRepo;
    }

    [HttpGet("/pendaftaran/index")]
    [Authorize(Roles = "PjlpUser")]
    public async Task<IActionResult> Index()
    {
        //DateTime closed = new(2022, 12, 12);

        //if (DateTime.Now > closed)
        //{
        //    return View("~/Views/Pendaftaran/TimeUp.cshtml");
        //}

        string? uid = ((ClaimsIdentity)User.Identity!).Claims.Where(c => c.Type == "sub").Select(c => c.Value).SingleOrDefault();
        string? email = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "email").Select(c => c.Value).SingleOrDefault();
        string? ktp = User.Identity.Name;
        string? nama = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "given_name").Select(c => c.Value).SingleOrDefault();

        Pegawai? peg = await repo.Pegawais
            .Where(x => x.NIK == ktp || x.Email == email)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .Include(p => p.Pendidikan)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .Include(x => x.DetailPjlp)
            .FirstOrDefaultAsync();

        string? lahir = "";
        bool isk2 = false;
        string? namabid = null;

        if (peg != null)
        {
            lahir = peg.TglLahir!.ToString();
            namabid = peg.Bidang.NamaBidang;

            if (peg!.DetailPjlp is null)
            {
                peg.DetailPjlp = new DetailPjlp();
            }

        } else
        {
            peg = new Pegawai { DetailPjlp = new DetailPjlp() };
        }

        TextInfo info = new CultureInfo("id_ID", false).TextInfo;

        return View(new PelamarVM
        {
            Pelamar = new Pelamar
            {
                UserId = Guid.Parse(uid!),
                UserEmail = email,
                NoKTP = ktp,
                Nama = nama!.ToUpper(),
                EventId = 1,
                Kelamin = peg!.Kelamin is null ? null : peg!.Kelamin!,
                GolonganDarah = peg!.GolDarah is null ? null : peg!.GolDarah!,
                // Tanggungan = peg!.DetailPjlp!.Tanggungan is null ? null : peg!.DetailPjlp!.Tanggungan,                
                IsK2 = isk2
            },
            Pegawai = peg,
            TanggalLahir = lahir != "" ? DateTime.Parse(lahir!).ToString("dd-MM-yyyy") : null,
            NamaAgama = peg!.AgamaID is null ? null : peg.Agama.NamaAgama,
            NamaPendidikan = peg!.PendidikanID is null ? null : peg.Pendidikan!.NamaPendidikan,
            Kelurahan = peg!.KelurahanID is null ? null : peg.Kelurahan!.NamaKelurahan,
            KecID = peg!.KelurahanID is null ? null : peg.Kelurahan!.KecamatanID,
            Kecamatan = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.NamaKecamatan,
            KabID = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.KabupatenID,
            Kabupaten = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.Kabupaten.NamaKabupaten,
            ProvID = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.Kabupaten.ProvinsiID,
            Provinsi = peg!.KelurahanID is null ? null : peg.Kelurahan!.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
            NamaBidang = namabid is null ? null : namabid

        });
    }

    [HttpGet("/pendaftaran/himbauan")]
    public IActionResult Himbauan()
    {
        return PartialView("~/Views/Pendaftaran/Himbauan.cshtml");
    }

    [HttpPost("/pendaftaran/store")]
    [Authorize(Roles = "PjlpUser")]
    public async Task<IActionResult> SavePendaftaran(PelamarVM model)
    {
        //DateTime closed = new(2022, 12, 12);

        //if (DateTime.Now > closed)
        //{
        //    return View("~/Views/Pendaftaran/TimeUp.cshtml");
        //}

        TextInfo info = new CultureInfo("id_ID", false).TextInfo;

        if (model.AddressIsSame)
        {
            model.Pelamar.NoKTP = User.Identity!.Name;
            model.Pelamar.DomKelurahanId = model.Pelamar.KelurahanId;
            model.Pelamar.DomAlamat = model.Pelamar.Alamat;
            model.Pelamar.DomRT = model.Pelamar.RT;
            model.Pelamar.DomRW = model.Pelamar.RW;
            model.Pelamar.DomKodePos = model.Pelamar.KodePos;
            model.Pelamar.AddressIsSame = model.AddressIsSame;
        }

        
        model.Pelamar.Nama = model.Pelamar.Nama.ToUpper();

        var pegawai = await pegRepo.Pegawais.Where(x => x.NIK == User.Identity!.Name).FirstOrDefaultAsync();

        if (pegawai is null)
        {
            model.Pelamar.IsNew = true;
        }

        model.Pelamar.StatusLamaranId = 1;
        model.Pelamar.TglLahir = DateOnly.Parse(model.TanggalLahir!, new CultureInfo("id-ID"));

        int isDewasa = GetAge(((DateOnly)model.Pelamar.TglLahir));

        if (isDewasa < 18)
        {
            model.Pelamar.StatusLamaranId = 6;
        }

        await pelamarRepo.SaveDataAsync(model.Pelamar);
        return Json(Result.Success());

        //return Json(Result.Failed());
    }

    [HttpGet("/pendaftaran/overview")]
    [Authorize(Roles = "PjlpUser")]
    public async Task<IActionResult> Overview()
    {
        Pelamar? data = await pelamarRepo.Pelamars
            .Where(x => x.NoKTP == User.Identity!.Name)
            .Include(a => a.Agama)
            .Include(b => b.Bidang)
            .Include(p => p.Pendidikan)
            .Include(j => j.Jabatan)
            .Include(e => e.Event)
            .Include(x => x.StatusLamaran)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .FirstOrDefaultAsync();

        if (data is null)
        {
            return RedirectToAction("Index");
        }        

        if (data is not null)
        {
            string? lahir = data.TglLahir.ToString();
            string? sim = "";

            int HarusUpload = await eventFileRepo.EventFiles
            .Where(x => x.JabatanID == data.JabatanId)
            .Where(x => x.IsNew == data.IsNew)
            .CountAsync();

            List<FilePelamar>? filePelamar = await fileRepo.FilePelamars
                .Where(x => x.PelamarId == data!.PelamarId)
                .Include(p => p.Persyaratan)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            int SudahUpload = filePelamar is null ? 0 : filePelamar.Count();
            int BelumUpload = HarusUpload - SudahUpload;

            string pasfoto = "";

            BelumUpload = BelumUpload < 0 ? 0 : BelumUpload;

            sim = data.TglAkhirSIM is not null ? data.TglAkhirSIM!.ToString() : "";

            if (filePelamar is not null)
            {
                if (filePelamar.Any(x => x.PersyaratanID == 2))
                {
#nullable disable
                    pasfoto = filePelamar.Where(x => x.PersyaratanID == 2).Select(x => x.FilePath + "/" + x.FileName).FirstOrDefault();
#nullable enable
                }
            }            

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
                AddressIsSame = (bool)data.AddressIsSame!,
                PasFoto = pasfoto != "" ? pasfoto : null,
                TotalUploaded = SudahUpload,
                TotalNotUploaded = BelumUpload,
                TglAkhirSIM = sim!
            });
        }

        return NotFound();
    }

    [HttpGet("/pendaftaran/files")]
    [Authorize(Roles = "PjlpUser")]
    public async Task<IActionResult> Berkas()
    {
        Pelamar? data = await pelamarRepo.Pelamars
            .Where(x => x.NoKTP == User.Identity!.Name)
            .FirstOrDefaultAsync();

        List<FilePelamar> filePelamars = await fileRepo.FilePelamars
            .Where(x => x.PelamarId == data!.PelamarId)
            .Include(p => p.Persyaratan)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return View("~/Views/Pendaftaran/Files.cshtml", new PelamarVM
        {
            Pelamar = data,
            Files = filePelamars
        });
    }

    [HttpPost("/pelamar/files/upload")]
    [Authorize(Roles = "PjlpUser, SysAdmin")]
    public async Task<IActionResult> UploadFiles(PelamarVM model)
    {
        string wwwPath = Uploads.Path;

        string path = Path.Combine(wwwPath, @"uploads/pelamar/", model.Pelamar.PelamarId.ToString());
        string thumbImg = path + @"/thumbnail";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fileExt = Path.GetExtension(model.Upload!.TheFile!.FileName);
        string fileName = GenerateRandomString() + fileExt;
        string realName = model.Upload!.TheFile!.FileName;
        string filePath = "/uploads/pelamar/" + model.Pelamar.PelamarId.ToString();
        string realPath = "/uploads/pelamar/" + model.Pelamar.PelamarId.ToString();        

        List<FilePelamar>? filePelamar = await fileRepo.FilePelamars
            .Where(x => x.PelamarId == model.Pelamar.PelamarId)
            .Include(p => p.Persyaratan)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        bool isNewFoto = false;
        bool isNew = true;
        string newPath = "";
        string oldID = "";

        isNewFoto = model.Upload.PersyaratanID == 2 ? true : false;

        if (filePelamar is not null)
        {
            if (filePelamar.Any(x => x.PersyaratanID == model.Upload.PersyaratanID))
            {
                var FileToDelete = filePelamar.Where(x => x.PersyaratanID == model.Upload.PersyaratanID)
                    .ToList();

                foreach(var p in FileToDelete)
                {
                    oldID = p!.FilePelamarID.ToString();
                    isNew = false;

                    await fileRepo.DeleteDataAsync(p!.FilePelamarID);

                    System.IO.File.Delete(path + "/" + p.FileName);
                    System.IO.File.Delete(thumbImg + "/" + p.FileName);
                }                
            }
        }

        FilePelamar file = new()
        {
            FilePelamarID = Guid.NewGuid(),
            FileName = fileName,
            RealName = realName,
            FileExtension = fileExt,
            FilePath = filePath,
            RealPath = realPath,
            PelamarId = model.Pelamar.PelamarId,
            PersyaratanID = model.Upload.PersyaratanID,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        newPath = file.FilePath + "/" + file.FileName;

        using (FileStream stream = new(Path.Combine(path, fileName), FileMode.Create))
        {
            model.Upload.TheFile.CopyTo(stream);
        }

        if (!fileExt.Contains("pdf"))
        {
            if (!Directory.Exists(thumbImg))
            {
                Directory.CreateDirectory(thumbImg);
            }

            filePath = "/uploads/pelamar/" + model.Pelamar.PelamarId.ToString() + "/thumbnail";

            Image image = Image.Load(model.Upload.TheFile.OpenReadStream());
            image.Mutate(x => x.Resize(600, 400));

            image.Save(thumbImg + "/" + fileName);
        } else
        {
            newPath = "/img/pdf.jpg";
        }

        await fileRepo.SaveDataAsync(file);

#nullable disable

        return Json(Result.SuccessUpload(isNew, isNewFoto, oldID, file.FilePelamarID.ToString(), model.Upload.TypeName, file.CreatedAt.ToString(), newPath));
    }

    [HttpPost("/pendaftaran/biodata/update")]
    [Authorize(Roles = "PjlpUser")]
    public async Task<IActionResult> UpdateBiodata(PelamarVM model) {
        var p = await pelamarRepo.Pelamars
                .Include(e => e.Event)
                .Where(x => x.PelamarId == model.Pelamar.PelamarId)
                .Select(x => new
                {
                    x.Event.EndDate
                }).FirstOrDefaultAsync();


        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        if (now > p.EndDate)
        {
            return Json(Result.TimeUp());
        }

        if (model.Pelamar.PelamarId != Guid.Empty) {
            model.Pelamar.TglLahir = DateOnly.Parse(model.TanggalLahir, new CultureInfo("id-ID"));

            await pelamarRepo.UpdateBiodata(model.Pelamar);

            return Json(Result.Success());
        }

        return Json(Result.Failed());
    }

    [HttpPost("/pendaftaran/alamat/update")]
    [Authorize(Roles = "PjlpUser")]
    public async Task<IActionResult> UpdateAlamat(PelamarVM model) {
        var p = await pelamarRepo.Pelamars
                .Include(e => e.Event)
                .Where(x => x.PelamarId == model.Pelamar.PelamarId)
                .Select(x => new
                {
                    x.Event.EndDate
                }).FirstOrDefaultAsync();


        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        if (now > p.EndDate)
        {
            return Json(Result.TimeUp());
        }

        model.Pelamar.AddressIsSame = model.AddressIsSame;

        if (model.Pelamar.PelamarId != Guid.Empty) {
            if (model.AddressIsSame) {
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

    [HttpPost("/pendaftaran/lainnya/update")]
    [Authorize(Roles = "PjlpUser")]
    public async Task<IActionResult> UpdateLainnya(PelamarVM model)
    {
        var p = await pelamarRepo.Pelamars
                .Include(e => e.Event)
                .Where(x => x.PelamarId == model.Pelamar.PelamarId)
                .Select(x => new
                {
                    x.Event.EndDate
                }).FirstOrDefaultAsync();


        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        if (now > p.EndDate)
        {
            return Json(Result.TimeUp());
        }

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

    [HttpGet("/pendaftaran/print")]
    public async Task<IActionResult> PrintBukti()
    {
        var pelamar = await pelamarRepo.Pelamars
            .Include(j => j.Jabatan)
            .Include(p => p.Pendidikan)
            .Include(b => b.Bidang)
            .Include(s => s.StatusLamaran)
            .Where(x => x.NoKTP == User.Identity.Name).FirstOrDefaultAsync();

        if (pelamar is null)
        {
            return NotFound();
        }

        string Foto = "/img/user.png";

        FilePelamar pasFoto = await fileRepo.FilePelamars            
            .Where(x => x.PelamarId == pelamar.PelamarId)
            .Where(x => x.PersyaratanID == 2)
            .FirstOrDefaultAsync();

        string StatusFile = "Belum Lengkap";

        int eventFiles = await eventFileRepo.EventFiles
            .Where(x => x.JabatanID == pelamar.JabatanId)
            .CountAsync();

        int totalUpload = await fileRepo.FilePelamars
            .Where(x => x.PelamarId == pelamar.PelamarId)
            .CountAsync();

        if (eventFiles == 0)
        {
            StatusFile = "Berkas wajib belum diatur oleh Admin/PPBJ";
        } else
        {
            int selisih = eventFiles - totalUpload;
            StatusFile = selisih < 1 ? "Berkas Lengkap" : "Belum Lengkap";
        }

        if (pasFoto is not null)
        {
            Foto = pasFoto.RealPath + "/" + pasFoto.FileName; 
        }

        return View("~/Views/Pendaftaran/Print.cshtml", new PrintModel
        {
            Pelamar = pelamar,
            ImageProfile = Foto,
            //StatusBerkas = StatusFile
            StatusBerkas = pelamar.StatusLamaran.NamaStatus
        });
    }

    private static int GetAge(DateOnly birthDate)
    {
        DateTime n = DateTime.Now; // To avoid a race condition around midnight
        int age = n.Year - birthDate.Year;

        if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
            age--;

        return age;
    }

    private static string GenerateRandomString()
    {
        int length = 60;

        StringBuilder builder = new();

        Random random = new();

        char letter;

        for (int i = 0; i < length; i++)
        {
            double flt = random.NextDouble();
            int shift = Convert.ToInt32(Math.Floor(25 * flt));
            letter = Convert.ToChar(shift + 65);
            builder.Append(letter);
        }

        return builder.ToString();
    }
}
