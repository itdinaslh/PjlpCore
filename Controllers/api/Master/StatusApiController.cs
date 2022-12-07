using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PjlpCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusApiController : ControllerBase
    {
        private readonly IStatus repo;

        public StatusApiController(IStatus repo) { this.repo = repo; }

        [HttpGet("/api/master/status/search")]
        public async Task<IActionResult> Search(string? term)
        {
            var data = await repo.Statuses
                .Where(k => !String.IsNullOrEmpty(term) ?
                    k.NamaStatus.ToLower().Contains(term.ToLower()) : true
                ).Select(s => new {
                    id = s.StatusId,
                    namaStatus = s.NamaStatus
                }).Take(10).ToListAsync();

            return Ok(data);
        }
    }
}
