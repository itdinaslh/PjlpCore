using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IProvinsiRepo {
    public IQueryable<Provinsi> Provinsis { get; }
}