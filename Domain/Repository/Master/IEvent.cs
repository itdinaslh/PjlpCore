using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IEvent
{
    IQueryable<Event> Events { get; }
}
