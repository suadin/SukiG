using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SukiG.Server.Database;
using SukiG.Shared.Model;
using System.Threading.Tasks;

namespace SukiG.Server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration config;
        private readonly DefaultDbContext dbContext;

        public UserController(IConfiguration config, DefaultDbContext dbContext)
        {
            this.config = config;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get(User user)
        {
            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}