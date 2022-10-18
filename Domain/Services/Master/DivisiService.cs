using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;
using PjlpCore.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Service;

public class DivisiService : IDivisiRepo {
    private AppDbContext context;

    public DivisiService(AppDbContext ctx) {
        context = ctx;
    }

    public IQueryable<Divisi> Divisis => context.Divisis;
    
    #nullable disable
    public async Task SaveDivisiAsync(DivisiViewModel model) {
        if(model.IsNew) {
            await context.AddAsync(model.Divisi);
        } else {
            Divisi div = await context.Divisis.FirstOrDefaultAsync(b => b.DivisiID == model.Divisi.DivisiID);
            div.NamaDivisi = model.Divisi.NamaDivisi.Trim();
            div.BidangID = model.Divisi.BidangID;
            context.Update(div);
        }

        await context.SaveChangesAsync();
    }
}