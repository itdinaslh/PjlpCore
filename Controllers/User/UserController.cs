using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;
using PjlpCore.Models;
using PjlpCore.Entity;
using PjlpCore.Helpers;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PjlpCore.Controllers;

[Authorize(Roles = "SysAdmin")]
public class UserController : Controller
{
    private readonly IBidangRepo bidangRepo;
    private readonly IUser userRepo;
    private readonly IHttpClientFactory _clientFactory;

    public UserController(IBidangRepo repo, IUser user, IHttpClientFactory factory) {
        bidangRepo = repo;
        userRepo = user;
        _clientFactory = factory;
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

    [HttpPost("/userbidang/manage/store")]
    public async Task<IActionResult> SaveDataUser(UserVM model, Guid[] bidangs) {
        try {
            var inject = new UserInject {                
                UserName = model.User.UserName,
                FullName = model.User.Name,
                Email = model.User.Email,
                Roles = model.User.RoleName,
                Password = model.Password
            };

            var jsonInject = JsonSerializer.Serialize(inject);

            var user = new StringContent(
                JsonSerializer.Serialize(inject),
                Encoding.UTF8,
                Application.Json
            );

            var client = _clientFactory.CreateClient("AuthClient");
            using HttpResponseMessage response = await client.PostAsync("/api/user/pjlp/store", user);

            if (!response.IsSuccessStatusCode) {
                return StatusCode(500, "Something Error...!");
            }

            return Json(Result.Success());

            // return Json(user);

        } catch (Exception) {
            throw;
        }
    }
}