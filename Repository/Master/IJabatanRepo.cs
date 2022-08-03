using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IJabatanRepo {
    IQueryable<Jabatan> Jabatans { get; }

    Task SaveJabatanAsync(Jabatan jabatan);
}