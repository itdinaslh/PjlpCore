using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Data;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Service;

public class PendidikanService : IPendidikanRepo {
    private AppDbContext context;

    public PendidikanService(AppDbContext ctx) => context = ctx;

    public IQueryable<Pendidikan> Pendidikans => context.Pendidikans;

    #nullable disable
    public async Task SavePendidikanAsync(Pendidikan pendidikan) {
        if (pendidikan.PendidikanID == 0) {
            await context.AddAsync(pendidikan);
        } else {
            Pendidikan data = context.Pendidikans.FirstOrDefault(a => a.PendidikanID == pendidikan.PendidikanID);
            data.NamaPendidikan = pendidikan.NamaPendidikan;

            context.Update(data);
        }

        await context.SaveChangesAsync();
    }
}