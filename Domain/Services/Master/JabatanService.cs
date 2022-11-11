using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;
using PjlpCore.Models.Master;

namespace PjlpCore.Services;

public class JabatanService : IJabatanRepo {
    private AppDbContext context;

    public JabatanService(AppDbContext ctx) {
        context = ctx;
    }

    public IQueryable<Jabatan> Jabatans => context.Jabatans;
    
    #nullable disable
    public async Task SaveJabatanAsync(JabatanViewModel model) {
        if(model.IsNew == true) {
            await context.AddAsync(model.Jabatan);
        } else {
            Jabatan div = await context.Jabatans.FindAsync(model.Jabatan.JabatanID);

            div.NamaJabatan = model.Jabatan.NamaJabatan.Trim();
            div.BidangID = model.Jabatan.BidangID;

            context.Update(div);
        }

        await context.SaveChangesAsync();
    }
}