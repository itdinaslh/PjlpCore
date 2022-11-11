using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class PegawaiService : IPegawai
{
    private readonly AppDbContext context;

    public PegawaiService(AppDbContext context) { this.context = context; }

    public IQueryable<Pegawai> Pegawais => context.Pegawais;
}
