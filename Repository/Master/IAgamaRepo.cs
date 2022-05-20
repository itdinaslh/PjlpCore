using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IAgamaRepo {
    IQueryable<Agama> Agamas { get; }

    Task SaveAgamaAsync(Agama agama);
}