using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class PegawaiService : IPegawai
{
    private readonly AppDbContext context;

    public PegawaiService(AppDbContext context) { this.context = context; }

    public IQueryable<Pegawai> Pegawais => context.Karyawans;

    public async Task UpdateAlamat(Pegawai peg)
    {
        Pegawai? data = await context.Karyawans.FindAsync(peg.PegawaiID);

        if (data is not null)
        {
            data.KelurahanID = peg.KelurahanID;
            data.AlamatKTP = peg.AlamatKTP;
            data.RtKTP= peg.RtKTP;
            data.RwKTP= peg.RwKTP;
            data.KelurahanDomID = peg.KelurahanDomID;
            data.RtDom = peg.RtDom;
            data.RwDom = peg.RwDom;
            data.AlamatDom= peg.AlamatDom;
            data.AddressIsSame = peg.AddressIsSame;

            context.Update(data);

            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateBiodata(Pegawai peg)
    {
        Pegawai? data = await context.Karyawans.FindAsync(peg.PegawaiID);

        if (data is not null)
        {
            data.NamaPegawai = peg.NamaPegawai;
            data.NoKK = peg.NoKK;
            data.AgamaID = peg.AgamaID;
            data.NoHP = peg.NoHP;
            data.TglLahir = peg.TglLahir;
            data.TempatLahir = peg.TempatLahir;
            data.Kelamin = peg.Kelamin;
            data.GolDarah = peg.GolDarah;
            data.UpdatedAt = DateTime.Now;

            context.Update(data);

            await context.SaveChangesAsync();
        }        
    }
}
