using PjlpCore.Entity;
using PjlpCore.Models.Wilayah;

namespace PjlpCore.Repository;

public interface IKelurahanRepo {
    IQueryable<Kelurahan> Kelurahans { get; }

    Task SaveDataAsync(KelurahanViewModel model);

}