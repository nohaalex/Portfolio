using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Portfolio.Models;
using System.Diagnostics;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult AboutMe()
        {
            return View();
        }

        public IActionResult Resume()
        {
            return View();
        }
        public IActionResult DownloadFile()
        {
            var memory = DownloadSinghFile("NOHA_ALEX-RESUME.pdf", "wwwroot\\MyFiles");
            return File(memory.ToArray(), "application/pdf", "NOHA_ALEX-RESUME.pdf");
        }
        private MemoryStream DownloadSinghFile(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Contact(Profile profile)
        {
            if(!ModelState.IsValid)
            {
                return View(profile);
            }

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(profile.EmailAddress)); 
            email.To.Add(MailboxAddress.Parse("alexnoha37@gmail.com"));
            email.Subject=profile.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = profile.Body
            };

            var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("alexnoha37@gmail.com", "flvtjwddesbrxddf");
            smtp.Send(email);
            smtp.Disconnect(true);

            ViewBag.Email = "The email has been sent";
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}