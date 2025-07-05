using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        public class ProjectDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public IFormFile? Image { get; set; }
            public string? GithubLink { get; set; }
            public string? LiveLink { get; set; }
            public string? TechStack { get; set; }
        }

        private string SaveImage(IFormFile image)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folderPath, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            // Return full relative path
            return "/images/" + uniqueName;
        }

        private void DeleteImage(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _context.Projects.ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProjectDto dto)
        {
            string? imagePath = null;
            if (dto.Image != null)
            {
                imagePath = SaveImage(dto.Image);
            }

            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                GithubLink = dto.GithubLink,
                LiveLink = dto.LiveLink,
                TechStack = dto.TechStack,
                ImageUrl = imagePath
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            project.Title = dto.Title;
            project.Description = dto.Description;
            project.GithubLink = dto.GithubLink;
            project.LiveLink = dto.LiveLink;
            project.TechStack = dto.TechStack;

            if (dto.Image != null)
            {
                DeleteImage(project.ImageUrl);
                project.ImageUrl = SaveImage(dto.Image);
            }

            await _context.SaveChangesAsync();
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            DeleteImage(project.ImageUrl);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
