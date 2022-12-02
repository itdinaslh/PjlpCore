using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class FIlePelamarService : IFilePelamar
{
    private readonly AppDbContext context;

    public FIlePelamarService (AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<FilePelamar> FilePelamars => context.FilePelamars;

    public Task DeleteDataAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task SaveDataAsync(FilePelamar file)
    {
        throw new NotImplementedException();
    }
}
