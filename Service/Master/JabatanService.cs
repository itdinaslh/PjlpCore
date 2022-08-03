using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;

namespace PjlpCore.Service;

public class JabatanService : IJabatanRepo {
    private AppDbContext context;

    public JabatanService(AppDbContext ctx) {
        context = ctx;
    }

    public IQueryable<Jabatan> Jabatans => context.Jabatans;
    
    #nullable disable
    public async Task SaveJabatanAsync(Jabatan jabatan) {
        if(jabatan.JabatanID == Guid.Empty || jabatan.JabatanID.ToString() == string.Empty) {
            await context.AddAsync(jabatan);
        } else {
            Jabatan jab = context.Jabatans.FirstOrDefault(b => b.JabatanID == jabatan.JabatanID);

            jab.NamaJabatan = jabatan.NamaJabatan.Trim();
            jab.BidangID = jabatan.BidangID;

            context.Update(jab);
        }

        await context.SaveChangesAsync();
    }
}