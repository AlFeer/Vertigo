using DAL.Data;
using DAL.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AppUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Vertigo 22", "vertigohotels@gmail.com"));
            message.To.Add(new MailboxAddress(user.UserName, user.Email));
            message.Subject = "Reservation mail";
            message.Body = new TextPart("plain")
            {
                Text = "Your booking was ready. Enjoy with your vacation"
            };

            using ( var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("vertigohotels@gmail.com", "tdtiujtvrkxxtads");
                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToAction("index", "home");
        }
    }
}
