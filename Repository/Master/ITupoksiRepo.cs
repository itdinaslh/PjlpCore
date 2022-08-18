using PjlpCore.Entity;
using PjlpCore.Models.Master;

namespace PjlpCore.Repository;

public interface ITupoksiRepo {
    IQueryable<Tupoksi> Tupoksis { get; }

    Task SaveTupoksiAsync(TupoksiViewModel tupoksi);
}