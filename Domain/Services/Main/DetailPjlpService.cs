using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class DetailPjlpService : IDetailPjlp
{
    private readonly AppDbContext context;

    public DetailPjlpService(AppDbContext context) { this.context = context; }

    public IQueryable<DetailPjlp> DetailPjlps => context.DetailPjlps;

    public async Task SaveDataAsync(DetailPjlp detail)
    {
        if (detail.DetailPjlpID == Guid.Empty)
        {
            await context.AddAsync(detail);
        } else
        {
            DetailPjlp? data = await context.DetailPjlps.FindAsync(detail.DetailPjlpID);

            if (data is not null)
            {
                data.Tanggungan = detail.Tanggungan;
                data.NoBPJSK = detail.NoBPJSK;
                data.NoSIM = detail.NoSIM;
                data.MasaBerlakuSIM = detail.MasaBerlakuSIM;
                data.JabatanID = detail.JabatanID;

                context.Update(data);
            }
        }

        await context.SaveChangesAsync();
    }
}
