using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SkillController(AppDbContext context) => _context = context;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Skills.ToListAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _context.Skills.FindAsync(id));
        [HttpPost] public async Task<IActionResult> Create(Skill skill) { _context.Skills.Add(skill); await _context.SaveChangesAsync(); return Ok(skill); }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Skill updated)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            skill.Name = updated.Name; skill.Level = updated.Level;
            await _context.SaveChangesAsync(); return Ok(skill);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            _context.Skills.Remove(skill); await _context.SaveChangesAsync(); return Ok("Deleted");
        }
    }

}
