using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class UserService : IUser
{
    private readonly AppDbContext context;

    public UserService(AppDbContext context) { this.context = context; }

    public IQueryable<User> Users => context.Users;
}
