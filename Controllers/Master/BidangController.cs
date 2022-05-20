using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace PjlpCore.Controllers;

public class BidangController : Controller {
    private IBidangRepo repo;
    private readonly IHubContext<BidangHub> _bidangHubContext;

    public BidangController(IBidangRepo bRepo, IHubContext<BidangHub> bidangHubContext) {
        repo = bRepo;
        _bidangHubContext = bidangHubContext;
    } 

    [HttpGet("/master/bidang")]
    public IActionResult Index() {
        return View("/Views/Master/Bidang/Index.cshtml");
    }


    [HttpGet("/master/bidang/create")]
    public IActionResult Create() => PartialView("/Views/Master/Bidang/AddEdit.cshtml", new Bidang { BidangID = Guid.Empty});

    [HttpGet("/master/bidang/edit")]
    public async Task<IActionResult> Edit(Guid bidangId) => PartialView("/Views/Master/Bidang/AddEdit.cshtml", 
        await repo.Bidangs.FirstOrDefaultAsync(b => b.BidangID == bidangId));

    [HttpPost("/master/bidang/save")]
    public async Task<IActionResult> SaveBidangAsync(Bidang bidang) {
        if (ModelState.IsValid) {
            await repo.SaveBidangAsync(bidang);
            await _bidangHubContext.Clients.All.SendAsync("TableUpdated");
            var result = new Dictionary<string, bool>();
            result.Add("success", true);
            return Json(result);
        } else {
            return PartialView("~/Views/Master/Bidang/AddEdit.cshtml", bidang);
        }
    }
}