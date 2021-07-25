using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SukiG.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MainController : Controller
    {
        private readonly IConfiguration config;

        public MainController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public ActionResult<string> Title() => config.GetValue<string>(nameof(Title));
    }
}
