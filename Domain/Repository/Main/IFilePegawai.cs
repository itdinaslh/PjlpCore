using PjlpCore.Domain.Entity.Main;

namespace PjlpCore.Repository;

public interface IFilePegawai
{
    IQueryable<FilePegawai> FilePegawais { get; }

    Task SaveDataAsync(FilePegawai file);
}
