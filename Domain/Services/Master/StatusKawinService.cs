using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class StatusKawinService : IStatusKawin
{
    private readonly AppDbContext context;

    public StatusKawinService(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<StatusKawin> StatusKawins => context.StatusKawins;

    public Task SaveDataAsync(StatusKawin statusKawin)
    {
        throw new NotImplementedException();
    }
}
