using PjlpCore.Repository;
using PjlpCore.Entity;
using PjlpCore.Data;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Service;

public class AgamaService : IAgamaRepo {
    private AppDbContext context;

    public AgamaService(AppDbContext ctx) => context = ctx;

    public IQueryable<Agama> Agamas => context.Agamas;

    #nullable disable
    public async Task SaveAgamaAsync(Agama agama) {
        if (agama.AgamaID == 0) {
            await context.AddAsync(agama);
        } else {
            Agama data = context.Agamas.FirstOrDefault(a => a.AgamaID == agama.AgamaID);
            data.NamaAgama = agama.NamaAgama;

            context.Update(data);
        }

        await context.SaveChangesAsync();
    }
}