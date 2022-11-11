using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Services;

public class LokasiKerjaService : ILokasiKerja
{
    private readonly AppDbContext context;

    public LokasiKerjaService(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<LokasiKerja> LokasiKerjas => context.LokasiKerjas;

    public async Task SaveDataAsync(LokasiKerja item)
    {
        if (item.LokasiKerjaID == 0)
        {
            await context.AddAsync(item);
        } else
        {
            LokasiKerja? data = await context.LokasiKerjas.FindAsync(item.LokasiKerjaID);

            if (data is not null)
            {
                data.NamaLokasi = item.NamaLokasi;
                data.UpdatedAt = DateTime.Now;
            }
        }

        await context.SaveChangesAsync();
    }
}
