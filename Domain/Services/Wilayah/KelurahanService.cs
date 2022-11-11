using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;
using PjlpCore.Models.Wilayah;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Services;

public class KelurahanService : IKelurahanRepo {
    private AppDbContext context;

    public KelurahanService(AppDbContext ctx) {
        context = ctx;
    }

    public IQueryable<Kelurahan> Kelurahans => context.Kelurahans;

    #nullable disable
    public async Task SaveDataAsync(KelurahanViewModel model) {
        if (model.IsNew) {
            model.Kelurahan.NamaKelurahan = model.Kelurahan.NamaKelurahan.ToUpper();
            await context.AddAsync(model.Kelurahan);
        } else {
            Kelurahan kel = await context.Kelurahans.FirstOrDefaultAsync(k => k.KelurahanID == model.ExistingID);
            kel.KelurahanID = model.Kelurahan.KelurahanID.Trim();
            kel.NamaKelurahan = model.Kelurahan.NamaKelurahan.ToUpper().Trim();
            kel.KecamatanID = model.Kelurahan.KecamatanID;
            kel.Latitude = model.Kelurahan.Latitude;
            kel.Longitude = model.Kelurahan.Longitude;

            context.Update(kel);
        }

        await context.SaveChangesAsync();
    }
}