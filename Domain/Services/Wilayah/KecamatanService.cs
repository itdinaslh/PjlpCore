using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;
using PjlpCore.Models.Wilayah;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Services;

public class KecamatanService : IKecamatanRepo {
    private AppDbContext context;

    public KecamatanService(AppDbContext ctx) => context = ctx;

    public IQueryable<Kecamatan> Kecamatans => context.Kecamatans;

    #nullable disable
    public async Task SaveDataAsync(KecamatanViewModel model) {
        if (model.IsNew) {
            model.Kecamatan.NamaKecamatan = model.Kecamatan.NamaKecamatan.ToUpper();
            await context.AddAsync(model.Kecamatan);
        } else {
            Kecamatan kec = await context.Kecamatans.FirstOrDefaultAsync(k => k.KecamatanID == model.ExistingID);
            kec.KecamatanID = model.Kecamatan.KecamatanID.Trim();
            kec.NamaKecamatan = model.Kecamatan.NamaKecamatan.ToUpper().Trim();
            kec.KabupatenID = model.Kecamatan.KabupatenID;
            kec.Latitude = model.Kecamatan.Latitude;
            kec.Longitude = model.Kecamatan.Longitude;

            context.Update(kec);
        }

        await context.SaveChangesAsync();
    }
}