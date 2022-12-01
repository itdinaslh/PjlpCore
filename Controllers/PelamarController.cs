using Microsoft.AspNetCore.Mvc;
using PjlpCore.Repository;
using PjlpCore.Models;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Entity;
using PjlpCore.Helpers;

namespace PjlpCore.Controllers;

public class PelamarController : Controller
{
    private readonly IPersyaratanRepo pRepo;
    private readonly IEventFile fileRepo;

    public PelamarController(IPersyaratanRepo pRepo, IEventFile fRepo) { 
        this.pRepo = pRepo;
        this.fileRepo = fRepo;
    }

    [HttpGet("/pelamar/files")]
    public async Task<IActionResult> FileWajib(Guid? jab, bool? isNew)
    {
        List<Persyaratan> dataList = new List<Persyaratan>();
        List<EventFile> events = new List<EventFile>();

        if (jab is not null && isNew is not null)
        {
            events = await fileRepo.EventFiles
                .Where(x => x.IsNew == isNew)
                .Where(x => x.JabatanID == jab)
                .ToListAsync();
        }

        dataList = await pRepo.Persyaratans.ToListAsync();

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
            ListSyarat = syaratList
        });;
    }

    [HttpPost("/pelamar/files/update")]
    public async Task<IActionResult> UpdateFileWajib(FileWajibVM model, int[] Files)
    {
        model.Files = Files;

        if (ModelState.IsValid)
        {
            await fileRepo.SaveDataAsync(model);

            return Json(Result.Success());
        }        

        
        return Json(Result.Failed());
    }
}
