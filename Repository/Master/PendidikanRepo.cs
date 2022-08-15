using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IPendidikanRepo {
    IQueryable<Pendidikan> Pendidikans { get; }

    Task SavePendidikanAsync(Pendidikan pendidikan);
}