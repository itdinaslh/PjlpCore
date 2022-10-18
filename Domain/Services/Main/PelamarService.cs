using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Service;

public class PelamarService : IPelamar
{
    private readonly AppDbContext context;

    public PelamarService(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Pelamar> Pelamars => context.Pelamars;
}
