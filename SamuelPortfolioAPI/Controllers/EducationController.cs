using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EducationController(AppDbContext context) => _context = context;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Educations.ToListAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _context.Educations.FindAsync(id));
        [HttpPost] public async Task<IActionResult> Create(Education e) { _context.Educations.Add(e); await _context.SaveChangesAsync(); return Ok(e); }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Education updated)
        {
            var e = await _context.Educations.FindAsync(id);
            if (e == null) return NotFound();
            e.Degree = updated.Degree; e.Institute = updated.Institute; e.Year = updated.Year; e.Grade = updated.Grade;
            await _context.SaveChangesAsync(); return Ok(e);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _context.Educations.FindAsync(id);
            if (e == null) return NotFound();
            _context.Educations.Remove(e); await _context.SaveChangesAsync(); return Ok("Deleted");
        }
    }
}
