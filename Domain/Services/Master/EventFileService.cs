using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Models;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Services;

public class EventFileService : IEventFile
{
    private readonly AppDbContext context;

    public EventFileService(AppDbContext context) { this.context = context; }

    public IQueryable<EventFile> EventFiles => context.EventFiles;

    public async Task SaveDataAsync(FileWajibVM model)
    {
        List<EventFile> events = new List<EventFile>();

        var files = await context.EventFiles
            .Where(x => x.JabatanID == model.JabatanID)
            .Where(x => x.IsNew == model.IsNew)
            .ToListAsync();

        if (files.Count > 0)
        {
            context.EventFiles.RemoveRange(files);
        }

        foreach (var p in model.Files!)
        {
            EventFile data = new EventFile
            {
                EventFileID = Guid.NewGuid(),
                EventID = 1,
                PersyaratanID = p,
                JabatanID = model.JabatanID,
                IsNew = model.IsNew
            };

            events.Add(data);

        }

        await context.AddRangeAsync(events);      

        await context.SaveChangesAsync();
    }

    public async Task RemoveDataAsync(List<EventFile> files)
    {
        context.RemoveRange(files);

        await context.SaveChangesAsync();
    }
}
