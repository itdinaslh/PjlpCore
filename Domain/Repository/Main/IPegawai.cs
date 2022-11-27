using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IPegawai
{
    IQueryable<Pegawai> Pegawais { get; }

    IQueryable<DetailPjlp> DetailPjlps { get; }

    Task UpdateBiodata(Pegawai peg);

    Task UpdateAlamat(Pegawai peg);

    Task UpdateDataLain(Pegawai peg);
}
