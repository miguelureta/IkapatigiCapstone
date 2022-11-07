using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace IkapatigiCapstone.Controllers
{
    
    
    public class EmailController : ControllerBase
    {
        private readonly IConfiguration _config;
        public EmailController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult SendEmail(string body, string temail)
        {
            var email = new MimeMessage();
            //Sender for this service
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));

            //Recipient of email
            email.To.Add(MailboxAddress.Parse("miguelblanco.ureta@benilde.edu.ph"));

            //Subject and content of email
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Html){ Text = body};

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            return RedirectToAction("Index", "Home");
        }
    }
}
