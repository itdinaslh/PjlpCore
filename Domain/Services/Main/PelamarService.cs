using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class PelamarService : IPelamar
{
    private readonly AppDbContext context;

    public PelamarService(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Pelamar> Pelamars => context.Pelamars;

    public async Task SaveDataAsync(Pelamar pelamar)
    {
        if (pelamar.PelamarId == Guid.Empty)
        {
            await context.AddAsync(pelamar);
        } else
        {

        }

        await context.SaveChangesAsync();
    }
}
