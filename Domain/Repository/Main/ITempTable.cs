using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface ITempTable
{
    IQueryable<TempTable> TempTables { get; }
}
