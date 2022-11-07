using PjlpCore.Entities;

namespace PjlpCore.Repositories;

public interface IStatusKawin
{
    IQueryable<StatusKawin> StatusKawins { get; }

    Task SaveDataAsync(StatusKawin statusKawin);
}
