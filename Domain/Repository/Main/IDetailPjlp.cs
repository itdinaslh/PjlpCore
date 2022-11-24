using PjlpCore.Entity;

namespace PjlpCore.Repository;

public interface IDetailPjlp
{
    IQueryable<DetailPjlp> DetailPjlps { get; }

    Task SaveDataAsync(DetailPjlp detail);
}
