using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IStatus
{
    IQueryable<Status> Statuses { get; }
}
