using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class TempTableService : ITempTable
{
    private readonly AppDbContext context;

    public TempTableService(AppDbContext context) { this.context = context; }

    public IQueryable<TempTable> TempTables => context.TempTables;
}
