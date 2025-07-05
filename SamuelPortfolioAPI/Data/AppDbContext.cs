using Microsoft.EntityFrameworkCore;
using SamuelPortfolioAPI.Models;

namespace SamuelPortfolioAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ProjectReaction> ProjectReactions { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Seeding Data

            modelBuilder.Entity<About>().HasData(new About
            {
                Id = 1,
                FullName = "Samuel Yaqoob",
                Bio = "I am a passionate full-stack .NET developer with React frontend skills.",
                Email = "yaqoobsamuel@outlook.com",
                Phone = "+92-307-5941955",
                ProfileImageUrl = "/images/profile.jpg"
            });

            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "ASP.NET Core", Level = "Expert" },
                new Skill { Id = 2, Name = "React.js", Level = "Intermediate" },
                new Skill { Id = 3, Name = "SQL Server", Level = "Expert" }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Title = "eCommerce Web App",
                    Description = "Full eCommerce system with cart, orders, admin area.",
                    ImageUrl = "/images/ecommerce.png",
                    GithubLink = "https://github.com/Samuel100-web",
                    LiveLink = "https://github.com/Samuel100-web",
                    TechStack = "ASP.NET Core, EF Core, React"
                }
            );

            modelBuilder.Entity<Education>().HasData(
                new Education { Id = 1, Degree = "ACCP Prime 2", Institute = "Aptech Learning SF Karachi", Year = "2025", Grade = "A" }
            );

            modelBuilder.Entity<Experience>().HasData(
                new Experience
                {
                    Id = 1,
                    Company = "Tech Soft",
                    Role = "Intern .NET Developer",
                    FromDate = new DateTime(2025, 5, 1),
                    ToDate = null,
                    Description = "Working on ASP.NET Core and React-based full stack projects."
                }
            );

            modelBuilder.Entity<Testimonial>().HasData(
                new Testimonial
                {
                    Id = 1,
                    Name = "Ali Raza",
                    Feedback = "Samuel is a skilled and dedicated developer.",
                    Designation = "Project Manager",
                    ProfileImageUrl = "/images/ali.jpg"
                }
            );

            modelBuilder.Entity<AdminUser>().HasData(new AdminUser
            {
                Id = 1,
                Email = "yaqoobsamuel@outlook.com",
                Password = "@Samuel777"
            });
        }
    }
}
