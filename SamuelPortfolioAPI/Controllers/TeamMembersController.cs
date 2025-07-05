using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;
using SamuelPortfolioAPI.Models.DTOs;

namespace SamuelPortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamMembersController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Create or Update — api/TeamMembers/save
        [HttpPost("save")]
        public async Task<IActionResult> SaveTeamMember([FromForm] TeamMemberSaveDto dto)
        {
            var imageFile = Request.Form.Files["imageFile"]; // image optional

            TeamMember member;

            if (dto.Id > 0)
            {
                // 🔁 Update
                member = await _context.TeamMembers.FindAsync(dto.Id);
                if (member == null) return NotFound();

                member.FullName = dto.FullName;
                member.Role = dto.Role;
                member.Bio = dto.Bio;
                member.LinkedInUrl = dto.LinkedInUrl;
                member.GitHubUrl = dto.GitHubUrl;
                member.Mobile = dto.Mobile;

                if (imageFile != null)
                {
                    // 🧹 Delete old image
                    if (!string.IsNullOrEmpty(member.ImageUrl))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", member.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                    }

                    // 💾 Save new image
                    string fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    member.ImageUrl = $"/images/{fileName}";
                }
            }
            else
            {
                // ➕ Create
                member = new TeamMember
                {
                    FullName = dto.FullName,
                    Role = dto.Role,
                    Bio = dto.Bio,
                    LinkedInUrl = dto.LinkedInUrl,
                    GitHubUrl = dto.GitHubUrl,
                    Mobile = dto.Mobile,
                    IsActive = true
                };

                if (imageFile != null)
                {
                    string fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    member.ImageUrl = $"/images/{fileName}";
                }

                _context.TeamMembers.Add(member);
            }

            await _context.SaveChangesAsync();
            return Ok(member);
        }


        // GET: api/TeamMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetTeamMembers()
        {
            var teamMembers = await _context.TeamMembers
                .Select(t => new TeamMemberDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Role = t.Role,
                    Bio = t.Bio,
                    ImageUrl = t.ImageUrl,
                    LinkedInUrl = t.LinkedInUrl,
                    GitHubUrl = t.GitHubUrl,
                    Mobile = t.Mobile,
                    IsActive = t.IsActive
                })
                .ToListAsync();

            return Ok(teamMembers);
        }

        // GET: api/TeamMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMemberDto>> GetTeamMember(int id)
        {
            var t = await _context.TeamMembers.FindAsync(id);
            if (t == null) return NotFound();

            return new TeamMemberDto
            {
                Id = t.Id,
                FullName = t.FullName,
                Role = t.Role,
                Bio = t.Bio,
                ImageUrl = t.ImageUrl,
                LinkedInUrl = t.LinkedInUrl,
                GitHubUrl = t.GitHubUrl,
                Mobile = t.Mobile,
                IsActive = t.IsActive
            };
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null) return NotFound();

            // delete image from folder
            if (!string.IsNullOrEmpty(member.ImageUrl))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", member.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
