using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IBidangRepo {
    IQueryable<Bidang> Bidangs { get; }

    Task SaveBidangAsync(Bidang bidang);
}