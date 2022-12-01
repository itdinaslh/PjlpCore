using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PjlpCore.Entity;
using PjlpCore.Repository;
using PjlpCore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace PjlpCore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventApiController : ControllerBase
    {
        private readonly IEventFile repo;

        public EventApiController (IEventFile repo)
        {
            this.repo = repo;
        }

        [HttpGet("/api/pelamar/files/check")]
        public async Task<IActionResult> CheckPersyaratan(Guid jab, bool isNew)
        {
            List<int> result = new List<int>();

            List<EventFile> eventFiles = await repo.EventFiles
                .Where(x => x.JabatanID == jab)
                .Where(x => x.IsNew == isNew)
                .ToListAsync();

            if (eventFiles.Count > 0)
            {
                foreach(var file in eventFiles)
                {
                    result.Add(file.PersyaratanID);
                }

                return Ok(result);
            }

            return new JsonResult(Result.Failed());
        }
    }
}
