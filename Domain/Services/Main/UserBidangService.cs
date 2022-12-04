using Microsoft.EntityFrameworkCore;
using PjlpCore.Data;
using PjlpCore.Entity;
using PjlpCore.Models;
using PjlpCore.Repository;

namespace PjlpCore.Services;

public class UserBidangService : IUserBidang
{
    private readonly AppDbContext context;

    public UserBidangService (AppDbContext context) { this.context = context; }

    public IQueryable<UserBidang> UserBidangs => context.UserBidangs;

    public async Task SaveDataAsync(UserVM data)
    {
        List<UserBidang> userBidangs = new List<UserBidang>();

        var userbidang = await context.UserBidangs
            .Where(x => x.UserID == data.User.UserID)
            .ToListAsync();

        if (userbidang.Count() > 0)
        {
            context.UserBidangs.RemoveRange(userbidang);
        }

        foreach(var p in data.Bidangs!)
        {
            UserBidang usr = new UserBidang
            {
                UserBidangID = Guid.NewGuid(),
                UserID = data.User.UserID,
                BidangID = p
            };

            userBidangs.Add(usr);
        }

        await context.AddRangeAsync(userBidangs);

        await context.SaveChangesAsync();
    }
}
