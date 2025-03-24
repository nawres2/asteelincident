using asteelincident.Server.Data;
using asteelincident.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asteelincident.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
        {
            var users = await _context.Users
               
                .ToListAsync();

            return Ok(users);
        }


    }
}
