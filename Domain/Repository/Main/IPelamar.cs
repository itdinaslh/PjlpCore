using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IPelamar
{
    IQueryable<Pelamar> Pelamars { get; }

    Task SaveDataAsync(Pelamar pelamar);

    Task UpdateBiodata(Pelamar pelamar);

    Task UpdateAlamat(Pelamar pelamar);

    Task UpdateLainnya(Pelamar pelamar);

    Task Pindahin(Pelamar pelamar);
}
