using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Models;
using PjlpCore.Repository;
using PjlpCore.Helpers;
using System.Globalization;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using PjlpCore.Domain.Entity.Main;
using PjlpCore.Domain.Repository;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin")]
public class PegawaiController : Controller
{
    private readonly IPegawai pegRepo;
    private readonly IFilePegawai fileRepo;

    public PegawaiController(IPegawai pRepo, IFilePegawai fRepo)
    {
        pegRepo = pRepo;
        fileRepo = fRepo;
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
            .Include(p => p.Pendidikan)
            .Include(k => k.Kelurahan!.Kecamatan.Kabupaten.Provinsi)
            .Include(d => d.KelurahanDom!.Kecamatan.Kabupaten.Provinsi)
            .FirstOrDefaultAsync();

        List<FilePegawai>? filePegawai = await fileRepo.FilePegawais
            .Where(x => x.PegawaiID == pid)
            .Include(p => p.Persyaratan)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        if (peg is not null)
        {
            string? lahir = peg.TglLahir.ToString();

            return View("~/Views/Main/Pegawai/PJLP/Details.cshtml", new PegawaiVM
            {
                Pegawai = peg,
                NamaAgama = peg.Agama.NamaAgama,
                NamaBidang = peg.Bidang.NamaBidang,                
                NamaPendidikan = peg.Pendidikan!.NamaPendidikan,
                TanggalLahir = DateTime.Parse(lahir!).ToString("dd-MM-yyyy"),
                Kelurahan = peg.KelurahanID is null ? "" : peg.Kelurahan!.NamaKelurahan,
                Kecamatan = peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.NamaKecamatan,
                KecID = peg.KelurahanID is null ? "" : peg.Kelurahan!.KecamatanID,
                KabID= peg.KelurahanID is null ? "" : peg.Kelurahan!.Kecamatan.KabupatenID,
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
                IsSame = peg.AddressIsSame ? true : false,
                Files = filePegawai
            });
        }

        return NotFound();
    }

    [HttpPost("/pegawai/biodata/update")]
    public async Task<IActionResult> UpdateBiodata(PegawaiVM model)
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

    [HttpPost("/pegawai/alamat/update")]
    public async Task<IActionResult> UpdateAlamat(PegawaiVM model)
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

    //[HttpPost("/pegawai/files/upload")]
    //public IActionResult UploadFile(PegawaiVM model)
    //{
    //    string wwwPath = @"/var/www/data";

    //    string path = Path.Combine(wwwPath, @"uploads/", model.Pegawai.PegawaiID.ToString());

    //    if (!Directory.Exists(path))
    //    {
    //        Directory.CreateDirectory(path);
    //    }

    //    //string fileName = Path.GetFileName(model.Upload.TheFile.FileName);        
        
    //    string fileExt = Path.GetExtension(model.Upload.TheFile.FileName);
    //    string fileName = GenerateRandomString() + fileExt;

    //    using (FileStream stream = new(Path.Combine(path, fileName), FileMode.Create))
    //    {
    //        model.Upload.TheFile.CopyTo(stream);
    //    }

    //    return Json(Result.Success());
    //}

    [HttpPost("/pegawai/files/upload")]
    public async Task<IActionResult> UploadFile(PegawaiVM model)
    {
        string wwwPath = @"C:\Data";

        string path = Path.Combine(wwwPath, @"uploads/", model.Pegawai.PegawaiID.ToString());
        string thumbImg = path + @"/thumbnail";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //string fileName = Path.GetFileName(model.Upload.TheFile.FileName);        

        string fileExt = Path.GetExtension(model.Upload.TheFile.FileName);
        string fileName = GenerateRandomString() + fileExt;
        string realName = model.Upload.TheFile.FileName;
        string filePath = "/uploads/" + model.Pegawai.PegawaiID.ToString();        
        

        if (!fileExt.Contains("pdf"))
        {
            if (!Directory.Exists(thumbImg))
            {
                Directory.CreateDirectory(thumbImg);
            }

            filePath = "/uploads/" + model.Pegawai.PegawaiID.ToString() + "/thumbnail";

            Image image = Image.Load(model.Upload.TheFile.OpenReadStream());
            image.Mutate(x => x.Resize(600, 400));

            image.Save(thumbImg + "/" + fileName);            
        }

        FilePegawai file = new FilePegawai
        {
            FileName = fileName,
            RealName = realName,
            FileExtension = fileExt,
            FilePath = filePath,
            PegawaiID = model.Pegawai.PegawaiID,
            PersyaratanID = model.Upload.PersyaratanID,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        using (FileStream stream = new(Path.Combine(path, fileName), FileMode.Create))
        {
            model.Upload.TheFile.CopyTo(stream);
        }

        await fileRepo.SaveDataAsync(file);

        return Json(Result.Success());
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
