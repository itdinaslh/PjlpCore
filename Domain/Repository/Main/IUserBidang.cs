using PjlpCore.Entity;
using PjlpCore.Models;

namespace PjlpCore.Repository;

public interface IUserBidang
{
    IQueryable<UserBidang> UserBidangs { get; }

    Task SaveDataAsync(UserVM data);
}
