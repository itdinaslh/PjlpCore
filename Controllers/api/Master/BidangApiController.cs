using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using PjlpCore.Entity;
using PjlpCore.Models.Master;
using PjlpCore.Repository;
using PjlpCore.Hubs;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers.api;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles = "SysAdmin")]
public class BidangApiController : Controller {
    private IBidangRepo repo;
    private readonly IUser userRepo;
    private readonly IUserBidang userBidangRepo;
    private readonly IHubContext<BidangHub> _bidangHubContext;

    public BidangApiController(IBidangRepo bRepo, IHubContext<BidangHub> bidangHubContext, IUser user, IUserBidang userBidang) {
        repo = bRepo;
        userRepo = user;
        userBidangRepo = userBidang;
        _bidangHubContext = bidangHubContext;
    }

    #nullable disable
    [HttpPost("/api/master/bidang")]
    public async Task<IActionResult> BidangTable() {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        int skip = start != null ? Convert.ToInt32(start) : 0;
        int recordsTotal = 0;

        var init = repo.Bidangs;

        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) {
            init = init.OrderBy(sortColumn + " " + sortColumnDirection);
        }

        if (!string.IsNullOrEmpty(searchValue)) {
            init = init.Where(a => a.NamaBidang.ToLower().Contains(searchValue.ToLower()) ||
                a.KepalaBidang.ToLower().Contains(searchValue.ToLower())
            );
        }

        recordsTotal = init.Count();

        var result = await init.Skip(skip).Take(pageSize).ToListAsync();

        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result};
        
        return Ok(jsonData);
    }

    [HttpGet("/api/master/bidang/spa")]
    public async Task<IActionResult> Bidangs(int pageSize, string query, int currentPage = 1) {
        var start = repo.Bidangs
            .Where(b => !String.IsNullOrEmpty(query) ? 
                b.NamaBidang.ToLower().Contains(query.ToLower()) || b.KepalaBidang.ToLower().Contains(query.ToLower()) : true
            );

        var data = await start
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(b => b.BidangID)
            .ToListAsync();

        var bidangs = new BidangResponse(data, start.Count());

        return Ok(bidangs);
    }

    [HttpPut("/api/master/bidang/save")]
    public async Task<IActionResult> SaveBidangApi(Bidang bidang) {
        if (ModelState.IsValid) {                      
            await repo.SaveBidangAsync(bidang);
            await _bidangHubContext.Clients.All.SendAsync("TableUpdated");
        } else {
            return BadRequest();
        }

        return Ok();
    }

    
    [HttpGet("/api/master/bidang/getbyid")]
    public async Task<JsonResult> GetDataByID(Guid bidangId) {
        var data = await repo.Bidangs.Select(x => new {
            bidangID = x.BidangID,
            namaBidang = x.NamaBidang,
            kepalaBidang = x.KepalaBidang
        }).FirstOrDefaultAsync(b => b.bidangID == bidangId);

        return Json(data);
    }

#nullable enable

    [HttpGet("/api/master/bidang/search")]    
    public async Task<IActionResult> SearchBidang(string? term) {
        var data = await repo.Bidangs 
            .Where(x => x.IsVisible == true)
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.NamaBidang.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.BidangID,
                namaBidang = s.NamaBidang
            }).ToListAsync();

        return Ok(data);
    }

    [HttpGet("/api/master/bidang/searchbycriteria")]
    public async Task<IActionResult> SearchBidangByRoles(string? term)
    {
        bool isBidang = User.IsInRole("PjlpAdmin") || User.IsInRole("PPBJ");

        List<Guid> bidangs = new();

        if (isBidang)
        {
            var user = await userRepo.Users.Where(x => x.UserName == User.Identity!.Name).FirstOrDefaultAsync();

            var bids = await userBidangRepo.UserBidangs
                .Where(x => x.UserID == user!.UserID)
                .ToListAsync();

            foreach (var p in bids)
            {
                bidangs.Add(p.BidangID);
            }
        }

        var data = await repo.Bidangs
            .Where(p => isBidang ? bidangs.Contains(p.BidangID) : true)
            .Where(b => b.IsVisible == true)
            .Where(k => !String.IsNullOrEmpty(term) ?
                k.NamaBidang.ToLower().Contains(term.ToLower()) : true
            ).Select(s => new {
                id = s.BidangID,
                namaBidang = s.NamaBidang
            }).ToListAsync();

        return Ok(data);
    }
}