using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IDivisiRepo {
    IQueryable<Divisi> Divisis { get; }

    Task SaveDivisiAsync(Divisi divisi);
}