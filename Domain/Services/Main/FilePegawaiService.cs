using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class FilePegawaiService : IFilePegawai
{
    private readonly AppDbContext context;

    public FilePegawaiService(AppDbContext context) { this.context = context; }

    public IQueryable<FilePegawai> FilePegawais => context.FilePegawais;

    public async Task SaveDataAsync(FilePegawai file)
    {
        if (file.FilePegawaiID == Guid.Empty)
        {
            await context.AddAsync(file);

            await context.SaveChangesAsync();
        }
    }
}
