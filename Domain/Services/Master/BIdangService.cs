using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Services;

public class BidangService : IBidangRepo {
    private AppDbContext context;

    public BidangService(AppDbContext ctx) {
        context = ctx;
    }

    public IQueryable<Bidang> Bidangs => context.Bidangs;
    
    #nullable disable
    public async Task SaveBidangAsync(Bidang bidang) {
        if(bidang.BidangID == Guid.Empty) {
            await context.AddAsync(bidang);
        } else {
            Bidang bid = await context.Bidangs.FindAsync(bidang.BidangID);

            bid.NamaBidang = bidang.NamaBidang.Trim();
            bid.KepalaBidang = bidang.KepalaBidang.Trim();

            context.Update(bid);
        }

        await context.SaveChangesAsync();
    }
}