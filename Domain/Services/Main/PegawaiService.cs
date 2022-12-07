using Microsoft.EntityFrameworkCore;
using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class PegawaiService : IPegawai
{
    private readonly AppDbContext context;

    public PegawaiService(AppDbContext context) { this.context = context; }

    public IQueryable<Pegawai> Pegawais => context.Pegawais;

    public IQueryable<DetailPjlp> DetailPjlps => context.DetailPjlps;

    public async Task UpdateAlamat(Pegawai peg)
    {
        Pegawai? data = await context.Pegawais.FindAsync(peg.PegawaiID);

        if (data is not null)
        {
            data.KelurahanID = peg.KelurahanID;
            data.AlamatKTP = peg.AlamatKTP;
            data.RtKTP= peg.RtKTP;
            data.RwKTP= peg.RwKTP;
            data.KodePosKTP = peg.KodePosKTP;
            data.KelurahanDomID = peg.KelurahanDomID;
            data.RtDom = peg.RtDom;
            data.RwDom = peg.RwDom;
            data.AlamatDom= peg.AlamatDom;
            data.KodePosDom = peg.KodePosDom;
            data.AddressIsSame = peg.AddressIsSame;

            context.Update(data);

            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateBiodata(Pegawai peg)
    {
        Pegawai? data = await context.Pegawais.FindAsync(peg.PegawaiID);

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

    public async Task UpdateDataLain(Pegawai peg)
    {
        Pegawai? data = await context.Pegawais.FindAsync(peg.PegawaiID);

        if (data is not null)
        {
            data.PendidikanID = peg.PendidikanID;
            data.JurusanPendidikan = peg.JurusanPendidikan;
            data.NamaSekolah = peg.NamaSekolah;
            data.NPWP = peg.NPWP;
            data.NoBPJS = peg.NoBPJS;
            data.StatusBPJS = peg.StatusBPJS;
            data.CabangBank = peg.CabangBank;
            data.NoRekening = peg.NoRekening;
            data.BidangID = peg.BidangID;

            DetailPjlp? detail = await context.DetailPjlps.Where(p => p.PegawaiID == data.PegawaiID).FirstOrDefaultAsync();

            if (detail is not null)
            {
                data.DetailPjlp!.Tanggungan = peg.DetailPjlp!.Tanggungan;
                data.DetailPjlp!.NoBPJSK = peg.DetailPjlp!.NoBPJSK;
                data.DetailPjlp!.NoSIM = peg.DetailPjlp!.NoSIM;
                data.DetailPjlp!.MasaBerlakuSIM = peg.DetailPjlp!.MasaBerlakuSIM;    
                data.DetailPjlp!.IsK2 = peg.DetailPjlp!.IsK2;
                data.DetailPjlp!.IsBlacklisted = peg.DetailPjlp!.IsBlacklisted;
            } else
            {
                data.DetailPjlp = new DetailPjlp {
                    DetailPjlpID = Guid.NewGuid(),
                    PegawaiID = data.PegawaiID,
                    Tanggungan = peg.DetailPjlp!.Tanggungan,
                    NoBPJSK = peg.DetailPjlp!.NoBPJSK,
                    NoSIM = peg.DetailPjlp!.NoSIM,
                    IsK2 = peg.DetailPjlp!.IsK2,
                    IsBlacklisted = peg.DetailPjlp!.IsBlacklisted,
                    MasaBerlakuSIM = peg.DetailPjlp!.MasaBerlakuSIM,
                    CreatedAt = DateTime.Now
                };                               

                await context.AddAsync(data.DetailPjlp);
            }

            context.Update(data);
        }

        await context.SaveChangesAsync();
    }
}
