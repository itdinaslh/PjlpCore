using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class FilePelamarService : IFilePelamar
{
    private readonly AppDbContext context;

    public FilePelamarService (AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<FilePelamar> FilePelamars => context.FilePelamars;

    public async Task DeleteDataAsync(Guid id)
    {
        FilePelamar? file = await context.FilePelamars.FindAsync(id);

        if (file != null)
        {
            context.Remove(file);

            await context.SaveChangesAsync();
        }
    }

    public async Task SaveDataAsync(FilePelamar file)
    {
        await context.AddAsync(file);

        await context.SaveChangesAsync();
    }
}
