using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Identity;
using IkapatigiCapstone.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace IkapatigiCapstone.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        //private readonly EmailController _econtrol;
        //private readonly SignInManager<User> _signInManager;
        public AccountController(/*UserManager<User> userManager, */ApplicationDbContext context, IConfiguration config/*, EmailController econtrolSignInManager<User> signInManager*/)
        {
            _context = context;
            _config = config;
            //_econtrol = econtrol;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        //[Route("google-login")]
        //public IActionResult GoogleLogin()
        //{
        //    var properties = new AuthenticationProperties{RedirectUri = Url.Action("GoogleResponse")};
        //    return Challenge(properties, Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme);
        //}

        //[ValidateAntiForgeryToken]
        //[Route("google-response")]
        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    //User newUser = new User();
        //    var claims = result.Principal.Identities.FirstOrDefault()
        //        .Claims.Select(claim => new
        //        {
        //            claim.Issuer,
        //            claim.OriginalIssuer,
        //            claim.Type,
        //            claim.Value
        //        });
        //    return Json(claims);
        //    //newUser.Email = result.Principal.Identities.FirstOrDefault()
        //    //    .Claims.Select(claim => new
        //    //{
        //    //    claim.Value
        //    //}).ToString();

        //    //await userManager.CreateAsync(newUser, );
        //    /*originally*/ 
        //    //return RedirectToAction("RedirectToLanding",claims);
        //}

        //First way to register, manual input into database, no hash
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        
        //public IActionResult Register(User u)
        //{
        //    var user = new User();
        //    user.Email = u.Email;
        //    user.Password = u.Password;
        //    _context.Users.Add(user);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        //Second way to register and login with hashing but requires datatype adjustments and column additions
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists");
            }
            hashPassword(request.Password, 
                out byte[] passwordHash, out byte[] passwordSalt);
            var token = CreateRandomToken();
            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = token
            };
            //_econtrol.SendEmail(token, user.Email);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //return RedirectToAction("Index");
            return RedirectToAction("Login");
            //Changed the page link. It goes to the Login now.
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(ModelState.IsValid)
            {
                if (user == null)
                {
                    ViewData["LoginMessage"] = "User does not exist";
                    
                    return View("Login");
                }
                if (!verifyhashPassword(request.Password,user.PasswordHash,user.PasswordSalt))
                {
                    ViewData["LoginMessage"] = "Invalid Login";
                    //return BadRequest("User does not exist");
                    return View("Login");
                }

                return RedirectToAction("MemberHome", "Home");
            }
            else
            {
                ViewData["LoginMessage"] = "Login Error";
                return View("Login");
            }
        }

        [HttpPost("verify")]//For account verification
        public async Task<IActionResult> Verify(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
            {
                return BadRequest("Invalid token");
            }
            //Assigns Member Role to Verifying User
            user.RoleId = 1;
            user.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Account");
        }


        //Code for hashing password
        private void hashPassword(string pw, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pw));
            }
            //var sha = SHA256.Create();
            //var asByteArray = Encoding.Default.GetBytes(pw);
            //var hash = sha.ComputeHash(asByteArray);
            //return Convert.ToBase64String(hash);
        }

        private bool verifyhashPassword(string pw, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new HMACSHA512(passSalt))
            {
                var computedpassHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pw));
                return computedpassHash.SequenceEqual(passHash);
            }
            //var sha = SHA256.Create();
            //var asByteArray = Encoding.Default.GetBytes(pw);
            //var hash = sha.ComputeHash(asByteArray);
            //return Convert.ToBase64String(hash);
        }
        //private bool checkPassword(string pw)
        //{
        //    string storedpass="";
        //    if(pw == hashPassword(storedpass))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public IActionResult RedirectToLanding()
        //{
        //    return RedirectToAction("Index","Home");
        //}
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        [Route("aLogin")]
        public ActionResult aLogin()
        {
            return View();
        }

        [HttpPost("aLogin")]
        public async Task<IActionResult> aLogin(AdminLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    ViewData["LoginMessage"] = "User does not exist";

                    return View("Login");
                }
                if (!verifyhashPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    ViewData["LoginMessage"] = "Invalid Login";
                    //return BadRequest("User does not exist");
                    return View("Login");
                }

                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewData["LoginMessage"] = "Login Error";
                return View("Login");
            }
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
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            return RedirectToAction("Index", "Home");
        }
    }
}
