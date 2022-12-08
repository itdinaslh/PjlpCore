using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;
using System.Drawing;

namespace PjlpCore.Services;

public class PelamarService : IPelamar
{
    private readonly AppDbContext context;

    public PelamarService(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Pelamar> Pelamars => context.Pelamars;

    public async Task SaveDataAsync(Pelamar pelamar)
    {
        if (pelamar.PelamarId == Guid.Empty)
        {
            await context.AddAsync(pelamar);
        }

        await context.SaveChangesAsync();
    }

    public async Task UpdateAlamat(Pelamar pelamar)
    {
        Pelamar? data = await context.Pelamars.FindAsync(pelamar.PelamarId);

        if (data is not null) {
            data.KelurahanId = pelamar.KelurahanId;
            data.RT = pelamar.RT;
            data.RW = pelamar.RW;
            data.KodePos = pelamar.KodePos;
            data.Alamat = pelamar.Alamat;
            data.DomKelurahanId = pelamar.DomKelurahanId;
            data.DomRT = pelamar.DomRT;
            data.DomRW = pelamar.DomRW;
            data.DomKodePos = pelamar.DomKodePos;
            data.DomAlamat = pelamar.DomAlamat;
            data.AddressIsSame = pelamar.AddressIsSame;

            context.Update(data);

            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateBiodata(Pelamar pelamar)
    {
        Pelamar? data = await context.Pelamars.FindAsync(pelamar.PelamarId);

        if (data is not null) {
            data.Nama = pelamar.Nama;
            data.NoKK = pelamar.NoKK;
            data.AgamaId = pelamar.AgamaId;
            data.TglLahir = pelamar.TglLahir;
            data.TempatLahir = pelamar.TempatLahir;
            data.Kelamin = pelamar.Kelamin;
            data.GolonganDarah = pelamar.GolonganDarah;

            context.Update(data);

            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateLainnya(Pelamar pelamar)
    {
        Pelamar? data = await context.Pelamars.FindAsync(pelamar.PelamarId);

        if (data is not null)
        {
            data.PendidikanId = pelamar.PendidikanId;
            data.JurusanSekolah = pelamar.JurusanSekolah;
            data.NamaSekolah = pelamar.NamaSekolah;
            data.NoNPWP = pelamar.NoNPWP;
            data.NoBPJS = pelamar.NoBPJS;
            data.StatusBPJS = pelamar.StatusBPJS;
            data.CabangRekening = pelamar.CabangRekening;
            data.NoRekening = pelamar.NoRekening;
            data.BidangId = pelamar.BidangId;
            data.Tanggungan = pelamar.Tanggungan;
            data.NoBPJSK = pelamar.NoBPJSK;
            data.NoSIM = pelamar.NoSIM;
            data.IsK2 = pelamar.IsK2;
            data.TglAkhirSIM = pelamar.TglAkhirSIM;
            data.JabatanId = pelamar.JabatanId;
            data.UpdatedAt = pelamar.UpdatedAt;

            context.Update(data);

            await context.SaveChangesAsync();
        }
    }

    public async Task Pindahin(Pelamar pelamar)
    {
        Pelamar? data = await context.Pelamars.FindAsync(pelamar.PelamarId);

        data!.IsNew = !data.IsNew;

        context.Update(data);

        await context.SaveChangesAsync();
    }
}
