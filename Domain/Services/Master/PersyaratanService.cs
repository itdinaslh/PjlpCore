using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Data;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Services;

public class PersyaratanService : IPersyaratanRepo {
    private AppDbContext context;

    public PersyaratanService(AppDbContext ctx) => context = ctx;

    public IQueryable<Persyaratan> Persyaratans => context.Persyaratans;

    #nullable disable
    public async Task SavePersyaratanAsync(Persyaratan persyaratan) {
        if (persyaratan.PersyaratanID == 0) {
            await context.AddAsync(persyaratan);
        } else {
            Persyaratan data = await context.Persyaratans.FindAsync(persyaratan.PersyaratanID);
            data.NamaPersyaratan = persyaratan.NamaPersyaratan;

            context.Update(data);
        }

        await context.SaveChangesAsync();
    }
}