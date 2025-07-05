using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonialController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TestimonialController(AppDbContext context) => _context = context;

        public class TestimonialDto
        {
            public string Name { get; set; }
            public string Feedback { get; set; }
            public string Designation { get; set; }
            public IFormFile? ImageFile { get; set; }
        }

        private string SaveImage(IFormFile image)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folderPath, uniqueName);
            using (var stream = new FileStream(filePath, FileMode.Create)) image.CopyTo(stream);
            return "/images/" + uniqueName;
        }

        private void DeleteImage(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
        }

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Testimonials.ToListAsync());
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TestimonialDto dto)
        {
            var t = new Testimonial
            {
                Name = dto.Name,
                Feedback = dto.Feedback,
                Designation = dto.Designation,
                ProfileImageUrl = dto.ImageFile != null ? SaveImage(dto.ImageFile) : null
            };
            _context.Testimonials.Add(t); await _context.SaveChangesAsync(); return Ok(t);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var t = await _context.Testimonials.FindAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] TestimonialDto dto)
        {
            var t = await _context.Testimonials.FindAsync(id);
            if (t == null) return NotFound();
            t.Name = dto.Name; t.Feedback = dto.Feedback; t.Designation = dto.Designation;
            if (dto.ImageFile != null)
            {
                DeleteImage(t.ProfileImageUrl);
                t.ProfileImageUrl = SaveImage(dto.ImageFile);
            }
            await _context.SaveChangesAsync(); return Ok(t);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _context.Testimonials.FindAsync(id);
            if (t == null) return NotFound();
            DeleteImage(t.ProfileImageUrl);
            _context.Testimonials.Remove(t); await _context.SaveChangesAsync(); return Ok("Deleted");
        }
    }

}
