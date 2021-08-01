using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SukiG.Server.Database;
using SukiG.Shared.Model;
using System.Linq;
using System.Threading.Tasks;

namespace SukiG.Server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly DefaultDbContext dbContext;

        public UserController(ILogger<UserController> logger, DefaultDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string name)
        {
            logger.LogDebug($"Try to get user with name='{name}'");
            var users = await dbContext.User.Where(user => user.Name == name).ToListAsync();
            if (users.Any())
            {
                var user = users.First(); // TODO: definitely not correct, need to use google identifier
                logger.LogDebug($"User found for name='{name}', ID={user.Id}. Return user to client...");
                return Ok(user);
            }

            logger.LogDebug($"User not found with name='{name}'!");
            return Ok(new User()); // empty user
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            // TODO: still buggy, name id empty
            logger.LogDebug($"Try to create user with name='{user.Name}'");
            await dbContext.User.AddAsync(user);
            await dbContext.Achivement.AddAsync(new Achivement { Name= "be cool", Description="hard stuff", Difficulty = Difficulty.Hard });
            await dbContext.SaveChangesAsync();
            logger.LogDebug($"User created successfully with id='{user.Id}'");
            return Ok(user);
        }
    }
}