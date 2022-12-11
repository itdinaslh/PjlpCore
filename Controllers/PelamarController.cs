using Microsoft.AspNetCore.Mvc;
using PjlpCore.Repository;
using PjlpCore.Models;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.IO.Compression;
using System.Globalization;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Data;
using ClosedXML.Excel;

namespace PjlpCore.Controllers;

[Authorize]
public class PelamarController : Controller
{
    private readonly IPersyaratanRepo pRepo;
    private readonly IEventFile eventRepo;
    private readonly IPelamar pelamarRepo;
    private readonly IFilePelamar fileRepo;
    private readonly IStatus statusRepo;
    private readonly IUser userRepo;
    private readonly IUserBidang userBidangRepo;

    public PelamarController(IPersyaratanRepo pRepo, IEventFile fRepo, IPelamar pelamarRepo, 
            IFilePelamar fileLamar, IStatus statusRepo, IUser userRepo, IUserBidang userBidangRepo) { 
        this.pRepo = pRepo;
        this.eventRepo = fRepo;
        this.pelamarRepo = pelamarRepo;
        this.fileRepo = fileLamar;
        this.statusRepo = statusRepo;
        this.userRepo = userRepo;
        this.userBidangRepo = userBidangRepo;
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

    public async Task<System.Data.DataTable> GetDataExport(bool isNew)
    {        
        bool isBidang = User.IsInRole("PjlpAdmin") || User.IsInRole("PPBJ");

        System.Data.DataTable dt = new System.Data.DataTable();
        dt.TableName = "Data_Pelamar";

        List<Guid> bidangs = new();
        List<UserBidang> bids = new();

        if (isBidang)
        {
            User? user = await userRepo.Users
                .Where(x => x.UserName == User.Identity!.Name)
                .FirstOrDefaultAsync();

            bids = await userBidangRepo.UserBidangs
                .Where(x => x.UserID == user!.UserID)
                .ToListAsync();

            foreach (var p in bids)
            {
                bidangs.Add(p.BidangID);
            }
        }

        var data = await pelamarRepo.Pelamars
            .Include(x => x.Agama)
            .Include(x => x.Kelurahan.Kecamatan.Kabupaten.Provinsi)
            .Include(x => x.KelurahanDom.Kecamatan.Kabupaten.Provinsi)
            .Include(x => x.Pendidikan)
            .Include(x => x.StatusLamaran)
            .Include(x => x.Jabatan)            
            .ThenInclude(x => x.Bidang)
            .Where(p => p.EventId == 1)
            .Where(p => p.IsNew == isNew)
            .Where(p => isBidang ? bidangs.Contains(p.BidangId) : true)
            .ToListAsync();
            

        // Add Columns
        dt.Columns.Add("No", typeof(int));
        dt.Columns.Add("Nama Lengkap", typeof(string));
        dt.Columns.Add("NIK", typeof(string));
        dt.Columns.Add("No. KK", typeof(string));
        dt.Columns.Add("No. NPWP", typeof(string));
        dt.Columns.Add("Agama", typeof(string));
        dt.Columns.Add("No. Telp", typeof(string));
        dt.Columns.Add("Tanggal Lahir", typeof(string));        
        dt.Columns.Add("Tempat Lahir", typeof(string));
        dt.Columns.Add("Usia", typeof(int));
        dt.Columns.Add("Jenis Kelamin", typeof(string));
        dt.Columns.Add("Alamat Sesuai KTP", typeof(string));
        dt.Columns.Add("Kelurahan", typeof(string));
        dt.Columns.Add("Kecamatan", typeof(string));
        dt.Columns.Add("Kota/Kabupaten", typeof(string));
        dt.Columns.Add("Provinsi", typeof(string));
        dt.Columns.Add("RT", typeof(string));
        dt.Columns.Add("RW", typeof(string));
        dt.Columns.Add("Kode Pos", typeof(string));
        dt.Columns.Add("Alamat Domisili", typeof(string));
        dt.Columns.Add("Kelurahan Domisili", typeof(string));
        dt.Columns.Add("Kecamatan Domisili", typeof(string));
        dt.Columns.Add("Kota/Kabupaten Domisili", typeof(string));
        dt.Columns.Add("Provinsi Domisili", typeof(string));
        dt.Columns.Add("RT Domisili", typeof(string));
        dt.Columns.Add("RW Domisili", typeof(string));
        dt.Columns.Add("Kode Pos Domisili", typeof(string));
        dt.Columns.Add("Pendidikan", typeof(string));
        dt.Columns.Add("Jurusan", typeof(string));
        dt.Columns.Add("Nama Sekolah", typeof(string));
        dt.Columns.Add("Tanggungan", typeof(string));
        dt.Columns.Add("No. BPJS Kesehatan", typeof(string));
        dt.Columns.Add("No. BPJS Ketenagakerjaan", typeof(string));
        dt.Columns.Add("Status BPJS", typeof(string));
        dt.Columns.Add("No. SIM/SIO", typeof(string));
        dt.Columns.Add("Berlaku Hingga", typeof(string));
        dt.Columns.Add("Cabang Bank DKI", typeof(string));
        dt.Columns.Add("No. Rekening Bank DKI", typeof(string));
        dt.Columns.Add("Bidang", typeof(string));
        dt.Columns.Add("Jabatan", typeof(string));
        dt.Columns.Add("Status K2", typeof(string));
        dt.Columns.Add("Status Verifikasi", typeof(string));
        dt.Columns.Add("File Sudah Upload", typeof(string));        

        // Add Rows To DataTable
        if (data is not null)
        {
            int x = 1;
            foreach (var p in data)
            {
                string kelamin = (bool)p.Kelamin! ? "Laki-laki" : "Perempuan";
                string statbpjs = "";

                if (p.StatusBPJS == "0")
                {
                    statbpjs = "Tidak Punya BPJS";
                } else if (p.StatusBPJS == "1")
                {
                    statbpjs = "Dinas Linkungan Hidup Prov. DKI Jakarta";
                } else if (p.StatusBPJS == "2")
                {
                    statbpjs = "Non Dinas Lingkungan Hidup Prov. DKI Jakarta";
                }

                string AlreadyFile = "";
                string myIsK2 = "TIDAK";

                if ((bool)p.IsK2!)
                    myIsK2 = "YA";

                var AllFiles = await fileRepo.FilePelamars
                    .Include(i => i.Persyaratan)
                    .Where(x => x.PelamarId == p.PelamarId)
                    .Where(x => x.Pelamar.EventId == 1)
                    .ToListAsync();

                if (AllFiles is not null)
                {
                    foreach (var file in AllFiles)
                    {
                        if (AlreadyFile == "")
                        {
                            AlreadyFile = file.Persyaratan.NamaPersyaratan;
                        }
                        else
                        {
                            AlreadyFile += ", " + file.Persyaratan.NamaPersyaratan;
                        }
                    }
                }

                dt.Rows.Add(
                    x,
                    p.Nama,
                    p.NoKTP,
                    p.NoKK,
                    p.NoNPWP,
                    p.Agama.NamaAgama,
                    p.Telp,
                    p.TglLahir,
                    p.TempatLahir,
                    GetAgeLastDec((DateOnly)p.TglLahir!),
                    kelamin,
                    p.Alamat,
                    p.Kelurahan.NamaKelurahan,
                    p.Kelurahan.Kecamatan.NamaKecamatan,
                    p.Kelurahan.Kecamatan.Kabupaten.NamaKabupaten,
                    p.Kelurahan.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
                    p.RT,
                    p.RW,
                    p.KodePos,
                    p.DomAlamat,
                    p.KelurahanDom.NamaKelurahan,
                    p.KelurahanDom.Kecamatan.NamaKecamatan,
                    p.KelurahanDom.Kecamatan.Kabupaten.NamaKabupaten,
                    p.KelurahanDom.Kecamatan.Kabupaten.Provinsi.NamaProvinsi,
                    p.DomRT,
                    p.DomRW,
                    p.DomKodePos,
                    p.Pendidikan.NamaPendidikan,
                    p.JurusanSekolah,
                    p.NamaSekolah,
                    p.Tanggungan,
                    p.NoBPJS,
                    p.NoBPJSK,
                    statbpjs,
                    p.NoSIM,
                    p.TglAkhirSIM,
                    p.CabangRekening,
                    p.NoRekening,
                    p.Jabatan.Bidang.NamaBidang,
                    p.Jabatan.NamaJabatan,
                    myIsK2,
                    p.StatusLamaran.NamaStatus,
                    AlreadyFile                    
                );

                x++;
            }
        }

        dt.AcceptChanges();

        return dt;
    }


    [HttpGet("/pelamar/excel/download")]
    [Authorize(Roles = "SysAdmin, PPBJ, PjlpAdmin, Kepeg")]
    public async Task<IActionResult> ExportToExcel(bool isNew = true)
    {
        System.Data.DataTable dt = await GetDataExport(isNew);

        string? filename = isNew == false ? "Data-Pelamar-Lama.xlsx" : "Data-Pelamar-Baru.xlsx";

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);

            using (MemoryStream stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
        }
    }

    private static int GetAgeLastDec(DateOnly birthDate)
    {
        DateTime n = new(2022, 12, 31);

        int age = n.Year - birthDate.Year;

        if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
            age--;

        return age;
    }
}
