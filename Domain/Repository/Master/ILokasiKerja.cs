using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface ILokasiKerja
{
    IQueryable<LokasiKerja> LokasiKerjas { get; }

    Task SaveDataAsync(LokasiKerja item);
}
