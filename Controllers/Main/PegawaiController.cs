using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Models;
using PjlpCore.Repository;
using PjlpCore.Helpers;
using System.Globalization;
using System.Text;
using System.IO;
using System.IO.Compression;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;


namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin, PPBJ, Kepeg")]
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

    [HttpGet("/pegawai/pns")]
    public IActionResult PNS()
    {
        return View("~/Views/Main/Pegawai/ASN/Index.cshtml");
    }


    [HttpPost("/pegawai/files/upload")]
    public async Task<IActionResult> UploadFile(PjlpVM model)
    {
        string wwwPath = Uploads.Path;        

        string path = Path.Combine(wwwPath, @"uploads/", model.Pegawai.PegawaiID.ToString());
        string thumbImg = path + @"/thumbnails";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //string fileName = Path.GetFileName(model.Upload.TheFile.FileName);        

        string fileExt = Path.GetExtension(model.Upload.TheFile!.FileName);
        string fileName = GenerateRandomString() + fileExt;
        string realName = model.Upload.TheFile.FileName;
        string filePath = "/uploads/" + model.Pegawai.PegawaiID.ToString();
        string realPath = "/uploads/" + model.Pegawai.PegawaiID.ToString();        

        List<FilePegawai>? filePegawai = await fileRepo.FilePegawais
            .Where(x => x.PegawaiID == model.Pegawai.PegawaiID)
            .Include(p => p.Persyaratan)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        bool isNewFoto = false;
        bool isNew = true;
        string newPath = "";
        string oldID = "";

        isNewFoto = model.Upload.PersyaratanID == 2 ? true : false;

        if (filePegawai is not null)
        {
            if (filePegawai.Any(x => x.PersyaratanID == model.Upload.PersyaratanID))
            {
                var FileToDelete = filePegawai.Where(x => x.PersyaratanID == model.Upload.PersyaratanID)                    
                    .FirstOrDefault();

                oldID = FileToDelete!.FilePegawaiID.ToString();
                isNew = false;                

                System.IO.File.Delete(path + "/" + FileToDelete.FileName);
                System.IO.File.Delete(thumbImg + "/" + FileToDelete.FileName);

                await fileRepo.DeleteDataAsync(FileToDelete!.FilePegawaiID);
            }
        }

        FilePegawai file = new()
        {
            FilePegawaiID = Guid.NewGuid(),
            FileName = fileName,
            RealName = realName,
            FileExtension = fileExt,
            FilePath = filePath,
            RealPath = realPath,
            PegawaiID = model.Pegawai.PegawaiID,
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

            filePath = realPath + "/thumbnails";

            Image image = Image.Load(model.Upload.TheFile.OpenReadStream());
            image.Mutate(x => x.Resize(600, 400));

            image.Save(thumbImg + "/" + fileName);
        }

        await fileRepo.SaveDataAsync(file);

        #nullable disable

        return Json(Result.SuccessUpload(isNew, isNewFoto, oldID, file.FilePegawaiID.ToString(), model.Upload.TypeName, file.CreatedAt.ToString(), newPath));

   
    }


    [HttpGet("/pegawai/files/download/single")]
    public async Task<IActionResult> DownloadSingle(Guid fileID)
    {
        var file = await fileRepo.FilePegawais.FirstOrDefaultAsync(x => x.FilePegawaiID == fileID);

        var isPdf = file.FileExtension.ToLower() == ".pdf";
        var mime = isPdf ? "application/pdf" : "image/png";

        var path = Uploads.Path + file.RealPath + "/" + file.FileName;

        return File(System.IO.File.ReadAllBytes(path), mime, System.IO.Path.GetFileName(path));
    }

    [HttpPost("/pegawai/files/download/selected")]
    public async Task DownloadSelected(Guid[] Files)
    {
        var files = await fileRepo.FilePegawais
            .Include(p => p.Pegawai)
            .Where(x => Files.Any(i => i == x.FilePegawaiID))
            .ToListAsync();

        var myID = files.FirstOrDefault();

        Response.ContentType = "application/octet-stream";
        Response.Headers.Add("Content-Disposition", "attachment; filename=" + myID.Pegawai.NamaPegawai + " - filtered.zip");

        using (ZipArchive archive = new ZipArchive(Response.BodyWriter.AsStream(), ZipArchiveMode.Create))
        {
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var entry = archive.CreateEntry(fileName);

                using var entryStream = entry.Open();
                using var fileStream = System.IO.File.OpenRead(Uploads.Path + "/uploads/" + file.PegawaiID.ToString() + "/" + fileName);

                await fileStream.CopyToAsync(entryStream);
            }
        }

    }

    [HttpPost("/pegawai/files/download/all")]
    public async Task DownloadAll(Guid myID)
    {
        var files = await fileRepo.FilePegawais
            .Include(p => p.Pegawai)
            .Where(x => x.PegawaiID == myID).ToListAsync();

        var theID = files.FirstOrDefault();

        Response.ContentType = "application/octet-stream";
        Response.Headers.Add("Content-Disposition", "attachment; filename=" + theID.Pegawai.NamaPegawai + ".zip");
        

        using (ZipArchive archive = new ZipArchive(Response.BodyWriter.AsStream(), ZipArchiveMode.Create))
        {
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var entry = archive.CreateEntry(fileName);

                using var entryStream = entry.Open();
                using var fileStream = System.IO.File.OpenRead(Uploads.Path + "/uploads/" + file.PegawaiID.ToString() + "/" + fileName);

                await fileStream.CopyToAsync(entryStream);
            }
        }
    }

#nullable enable

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
