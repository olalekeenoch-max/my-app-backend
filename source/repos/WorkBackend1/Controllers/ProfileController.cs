using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkBackend1.Data;

namespace WorkBackend1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        // GET user profile
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            return Ok(new { user.Id, user.Email, user.Role });
        }

        // PUT update email
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(int userId, [FromBody] UpdateProfileDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.Email = dto.Email;
            await _context.SaveChangesAsync();
            return Ok("Profile updated successfully");
        }
    }

    public class UpdateProfileDto
    {
        public string Email { get; set; } = string.Empty;
    }
}
