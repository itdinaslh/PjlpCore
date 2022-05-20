using PjlpCore.Entity;
using PjlpCore.Models.Wilayah;

namespace PjlpCore.Repository;

public interface IKabupatenRepo {
    IQueryable<Kabupaten> Kabupatens { get; }

    Task SaveKabupatenAsync(KabupatenViewModel kabupaten);
}