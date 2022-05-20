using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Data;

namespace PjlpCore.Service;

public class ProvinsiService : IProvinsiRepo {
    private AppDbContext context;

    public ProvinsiService(AppDbContext ctx) => context = ctx;

    public IQueryable<Provinsi> Provinsis => context.Provinsis;
}