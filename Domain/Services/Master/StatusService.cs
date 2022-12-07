using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class StatusService : IStatus
{
    private readonly AppDbContext context;

    public StatusService (AppDbContext context) {  this.context = context; }

    public IQueryable<Status> Statuses => context.Statuses;
}
