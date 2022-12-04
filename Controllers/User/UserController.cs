using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Models;
using PjlpCore.Entity;
using PjlpCore.Helpers;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin")]
public class UserController : Controller
{
    private readonly IBidangRepo bidangRepo;
    private readonly IUser userRepo;

    public UserController(IBidangRepo repo, IUser user) {
        bidangRepo = repo;
        userRepo = user;
    }

    [HttpGet("/userbidang/list")]
    public IActionResult Index() {
        return View("~/Views/User/Index.cshtml");
    }

    [HttpGet("/userbidang/manage")]
    public async Task<IActionResult> Manage(Guid? userID) {
        var bidangs = await bidangRepo.Bidangs.ToListAsync();
        List<SelectedList> bidangList = new List<SelectedList>();

        double bagi = bidangs.Count() / 2;
        int batas = bagi % 2 == 0 ? (int)bagi : (int)Math.Ceiling(bagi);

        if (userID is null) {
            foreach(var bidang in bidangs) {
                var list = new SelectedList {
                    OtherID = bidang.BidangID,
                    Text = bidang.NamaBidang,
                    Selected = false
                };
                
                bidangList.Add(list);
            }

            return View(new UserVM {
                User = new User(),
                ListBidang = bidangList,
                Batas = batas
            });
        }

        User? user = await userRepo.Users.FirstOrDefaultAsync(x => x.UserID == userID);

        return View(new UserVM {
            User = user!,            
            IsNew = false
        });
    }


    public IActionResult SaveDataUser(UserVM model) {
        if (ModelState.IsValid) {
            
        }

        return Json(Result.Success());
    }
}