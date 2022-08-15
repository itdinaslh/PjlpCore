using PjlpCore.Entity;
using PjlpCore.Models.Master;

namespace PjlpCore.Repository;

public interface IDivisiRepo {
    IQueryable<Divisi> Divisis { get; }

    Task SaveDivisiAsync(DivisiViewModel model);
}