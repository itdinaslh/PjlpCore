using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class UserService : IUser
{
    private readonly AppDbContext context;

    public UserService(AppDbContext context) { this.context = context; }

    public IQueryable<User> Users => context.Users;

    public async Task SaveDataAsync(User user)
    {
        if (user.UserID != Guid.Empty) {
            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }
    }
}
