using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IPegawai
{
    IQueryable<Pegawai> Pegawais { get; }
}
