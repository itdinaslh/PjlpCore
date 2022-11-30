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


namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin, PjlpAdmin, PPBJ")]
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
        string thumbImg = path + @"/thumbnail";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //string fileName = Path.GetFileName(model.Upload.TheFile.FileName);        

        string fileExt = Path.GetExtension(model.Upload.TheFile!.FileName);
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

                await fileRepo.DeleteDataAsync(FileToDelete!.FilePegawaiID);

                System.IO.File.Delete(path + "/" + FileToDelete.FileName);
                System.IO.File.Delete(thumbImg + "/" + FileToDelete.FileName);
            }
        }

        FilePegawai file = new()
        {
            FilePegawaiID = Guid.NewGuid(),
            FileName = fileName,
            RealName = realName,
            FileExtension = fileExt,
            FilePath = filePath,
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

        await fileRepo.SaveDataAsync(file);

        #nullable disable

        return Json(Result.SuccessUpload(isNew, isNewFoto, oldID, file.FilePegawaiID.ToString(), model.Upload.TypeName, file.CreatedAt.ToString(), newPath));

   
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
