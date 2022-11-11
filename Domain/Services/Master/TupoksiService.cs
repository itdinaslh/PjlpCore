using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;
using PjlpCore.Models.Master;

namespace PjlpCore.Services;

public class TupoksiService : ITupoksiRepo {
    private AppDbContext context;

    public TupoksiService(AppDbContext ctx) {
        context = ctx;
    }

    public IQueryable<Tupoksi> Tupoksis => context.Tupoksis;
    
    #nullable disable
    public async Task SaveTupoksiAsync(TupoksiViewModel model) {
        if(model.IsNew == true) {
            await context.AddAsync(model.Tupoksi);
        } else {
            Tupoksi div = await context.Tupoksis.FindAsync(model.Tupoksi.TupoksiID);

            div.NamaTupoksi = model.Tupoksi.NamaTupoksi.Trim();
            div.DivisiID = model.Tupoksi.DivisiID;

            context.Update(div);
        }

        await context.SaveChangesAsync();
    }
}