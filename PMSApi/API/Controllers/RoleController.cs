using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return Ok(roles);
        }

        [HttpPost("create-new-role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest(new { error = "Role name is required" });
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    return Ok(new { result = $"Role {roleName} has been added" });
                }
                else
                {
                    return BadRequest(new { error = $"Failed to create role {roleName}" });
                }
            }

            return BadRequest(new { error = "Role already exists" });
        }

        //[HttpPost]
        //public async Task<IActionResult> AddUserToRole(string username, string roleName)
        //{
        //    var user = await _userManager.FindByNameAsync(username);

        //    if(user is null) return BadRequest(new {})
        //}

    }
}
