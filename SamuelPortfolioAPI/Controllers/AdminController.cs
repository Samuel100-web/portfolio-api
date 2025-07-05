using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                    return BadRequest(new { message = "Email or password is missing" });

                var user = _context.AdminUsers
                    .FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);

                if (user == null)
                {
                    return Unauthorized(new { success = false, message = "Invalid email or password" });
                }

                return Ok(new { success = true, message = "Login successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }


    public class AdminLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
