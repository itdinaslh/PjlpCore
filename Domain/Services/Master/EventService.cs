using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class EventService : IEvent
{
    private AppDbContext context;

    public EventService(AppDbContext context) { this.context = context; }

    public IQueryable<Event> Events => context.Events;
}
