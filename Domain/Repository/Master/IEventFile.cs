using PjlpCore.Entity;
using PjlpCore.Models;

namespace PjlpCore.Repository;

public interface IEventFile
{
    IQueryable<EventFile> EventFiles { get; }

    Task SaveDataAsync(FileWajibVM model);
}
