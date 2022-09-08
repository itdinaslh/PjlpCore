using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IPelamar
{
    IQueryable<Pelamar> Pelamars { get; }
}
