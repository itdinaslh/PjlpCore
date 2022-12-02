using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IFilePelamar
{
    IQueryable<FilePelamar> FilePelamars { get; }

    Task SaveDataAsync(FilePelamar file);

    Task DeleteDataAsync(Guid id);
}
