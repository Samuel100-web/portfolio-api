using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SamuelPortfolioAPI.Data;
using SamuelPortfolioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SamuelPortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public ContactController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Contacts.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Submit(Contact contact)
        {
            contact.CreatedAt = DateTime.UtcNow;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Optional: Send email to admin (configure SMTP first)
            // await SendEmailToAdmin(contact);

            return Ok(new { message = "Your message has been received." });
        }

        private async Task SendEmailToAdmin(Contact contact)
        {
            var smtpClient = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"] ?? "587"),
                Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress(contact.Email),
                Subject = "New Contact Message",
                Body = $"Name: {contact.Name}\nEmail: {contact.Email}\nMessage: {contact.Message}",
                IsBodyHtml = false,
            };

            mail.To.Add(_config["Smtp:AdminEmail"]);
            await smtpClient.SendMailAsync(mail);
        }
    }
}
