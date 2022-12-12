using Microsoft.AspNetCore.Mvc;

namespace PjlpCore.Controllers;

public class ManageController : Controller
{
    [HttpGet("/manage/check/date")]
    public DateTime CheckDate()
    {
        return DateTime.Now;
    }
}
