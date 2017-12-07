using Microsoft.AspNetCore.Mvc;

namespace Kaolin.Api.Rail.Controllers
{
    public class RailControllerBase : Controller
    {
        protected IActionResult SessionExpired()
            => StatusCode(410, new { error = "Session expired" });
    }
}
