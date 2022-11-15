using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IPegawai
{
    IQueryable<Pegawai> Pegawais { get; }

    Task UpdateBiodata(Pegawai peg);

    Task UpdateAlamat(Pegawai peg);
}
