using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;
using PjlpCore.Models.Wilayah;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Service;

public class KabupatenService : IKabupatenRepo {
    private AppDbContext context;
    
    public KabupatenService(AppDbContext ctx) => context = ctx;

    public IQueryable<Kabupaten> Kabupatens => context.Kabupatens;

    #nullable disable
    public async Task SaveKabupatenAsync(KabupatenViewModel model) {
        if (model.IsNew == true) {
            model.Kabupaten.NamaKabupaten = model.Kabupaten.NamaKabupaten.ToUpper();
            await context.AddAsync(model.Kabupaten);
        } else {
            Kabupaten kab = await context.Kabupatens.FirstOrDefaultAsync(k => k.KabupatenID == model.ExistingID);
            kab.KabupatenID = model.Kabupaten.KabupatenID;
            kab.NamaKabupaten = model.Kabupaten.NamaKabupaten.ToUpper();
            kab.ProvinsiID = model.Kabupaten.ProvinsiID;
            kab.Latitude = model.Kabupaten.Latitude;
            kab.Longitude = model.Kabupaten.Longitude;

            context.Update(kab);
        }

        await context.SaveChangesAsync();
    }
}