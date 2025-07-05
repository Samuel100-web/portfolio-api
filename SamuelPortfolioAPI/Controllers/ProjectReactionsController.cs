using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectReactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProjectReactionController(AppDbContext context) => _context = context;

        // GET: Get total likes/dislikes + average rating
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetReactions(int projectId)
        {
            var reactions = await _context.ProjectReactions
                .Where(r => r.ProjectId == projectId)
                .ToListAsync();

            var likes = reactions.Count(r => r.IsLike == true);
            var dislikes = reactions.Count(r => r.IsLike == false);
            var avgRating = reactions.Count > 0 ? reactions.Average(r => r.Rating) : 0;

            return Ok(new { likes, dislikes, avgRating });
        }

        // POST: Submit reaction
        [HttpPost]
        public async Task<IActionResult> PostReaction([FromBody] ProjectReaction model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid reaction data");

            var reaction = new ProjectReaction
            {
                ProjectId = model.ProjectId,
                IsLike = model.IsLike,
                Rating = model.Rating
            };

            _context.ProjectReactions.Add(reaction);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reaction saved!" });
        }
    }
}
