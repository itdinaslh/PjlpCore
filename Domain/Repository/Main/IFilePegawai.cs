using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IFilePegawai
{
    IQueryable<FilePegawai> FilePegawais { get; }

    Task SaveDataAsync(FilePegawai file);

    Task DeleteDataAsync(Guid id);
}
