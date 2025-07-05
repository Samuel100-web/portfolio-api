using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    public class AboutDto
    {
        public string FullName { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }


    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Abouts.FirstOrDefaultAsync());

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] AboutDto model, IFormFile? imageFile)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null) return NotFound();

            // Update basic fields
            about.FullName = model.FullName;
            about.Bio = model.Bio;
            about.Email = model.Email;
            about.Phone = model.Phone;

            // Handle image upload
            if (imageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                about.ProfileImageUrl = "/images/" + fileName;
            }

            await _context.SaveChangesAsync();
            return Ok(about);
        }


    }
}
