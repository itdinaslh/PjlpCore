using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;

namespace PjlpCore.Service;

public class DivisiService : IDivisiRepo {
    private AppDbContext context;

    public DivisiService(AppDbContext ctx) {
        context = ctx;
    }

    public IQueryable<Divisi> Divisis => context.Divisis;
    
    #nullable disable
    public async Task SaveDivisiAsync(Divisi divisi) {
        if(divisi.DivisiID == Guid.Empty || divisi.DivisiID.ToString() == string.Empty) {
            await context.AddAsync(divisi);
        } else {
            Divisi div = context.Divisis.FirstOrDefault(b => b.DivisiID == divisi.DivisiID);

            div.NamaDivisi = divisi.NamaDivisi.Trim();
            div.BidangID = divisi.BidangID;

            context.Update(div);
        }

        await context.SaveChangesAsync();
    }
}