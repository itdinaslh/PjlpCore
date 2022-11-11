using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IStatusKawin
{
    IQueryable<StatusKawin> StatusKawins { get; }

    Task SaveDataAsync(StatusKawin statusKawin);
}
