using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class FilePegawaiService : IFilePegawai
{
    private readonly AppDbContext context;

    public FilePegawaiService(AppDbContext context) { this.context = context; }

    public IQueryable<FilePegawai> FilePegawais => context.FilePegawais;

    public async Task DeleteDataAsync(Guid id)
    {
        FilePegawai? file = await context.FilePegawais.FindAsync(id);

        if (file != null)
        {
            context.Remove(file);

            await context.SaveChangesAsync();
        }
    }

    public async Task SaveDataAsync(FilePegawai file)
    {
        await context.AddAsync(file);

        await context.SaveChangesAsync();
    }
}
