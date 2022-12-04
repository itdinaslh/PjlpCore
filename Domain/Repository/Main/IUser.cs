using PjlpCore.Entity;

namespace PjlpCore.Repository
{
    public interface IUser
    {
        IQueryable<User> Users { get; }

        Task SaveDataAsync(User user);
    }
}
