using PjlpCore.Entity;
using PjlpCore.Models.Master;

namespace PjlpCore.Repository;

public interface IJabatanRepo {
    IQueryable<Jabatan> Jabatans { get; }

    Task SaveJabatanAsync(JabatanViewModel jabatan);
}