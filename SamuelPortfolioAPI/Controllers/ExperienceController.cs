using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ExperienceController(AppDbContext context) => _context = context;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Experiences.ToListAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _context.Experiences.FindAsync(id));
        [HttpPost] public async Task<IActionResult> Create(Experience e) { _context.Experiences.Add(e); await _context.SaveChangesAsync(); return Ok(e); }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Experience updated)
        {
            var e = await _context.Experiences.FindAsync(id);
            if (e == null) return NotFound();
            e.Company = updated.Company; e.Role = updated.Role; e.FromDate = updated.FromDate; e.ToDate = updated.ToDate; e.Description = updated.Description;
            await _context.SaveChangesAsync(); return Ok(e);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Experiences.FindAsync(id);
            if (e == null) return NotFound();
            _context.Experiences.Remove(e); await _context.SaveChangesAsync(); return Ok("Deleted");
        }
    }
}
