using PjlpCore.Entity;

namespace PjlpCore.Repository
{
    public interface IUser
    {
        IQueryable<User> Users { get; }
    }
}
