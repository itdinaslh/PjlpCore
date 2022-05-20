using PjlpCore.Entity;
using PjlpCore.Models.Wilayah;

namespace PjlpCore.Repository;

public interface IKecamatanRepo {
    IQueryable<Kecamatan> Kecamatans { get; }

    Task SaveDataAsync(KecamatanViewModel kecamatan);
}