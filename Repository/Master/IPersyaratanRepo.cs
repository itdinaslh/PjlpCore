using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IPersyaratanRepo {
    IQueryable<Persyaratan> Persyaratans { get; }

    Task SavePersyaratanAsync(Persyaratan persyaratan);
}