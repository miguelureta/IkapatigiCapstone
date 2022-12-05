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
    //[Authorize]
    public class AccountController : Controller
    {
        //private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _hcontext;
        //private readonly IEmailService _emailService;
        //private readonly SignInManager<User> _signInManager;
        public AccountController(/*UserManager<User> userManager, */ApplicationDbContext context, IConfiguration config/*, SignInManager<User> signInManager*/, IHttpContextAccessor hcontext)
        {
            _context = context;
            _config = config;
            _hcontext = hcontext;
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
        //[AllowAnonymous]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        //[AllowAnonymous]
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
        //[AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                ViewBag["RegisterError"] = "User already exists";
                return View("Register");
            }
            hashPassword(request.Password, 
                out byte[] passwordHash, out byte[] passwordSalt);
            var token = CreateRandomToken();

            //Email section start
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = token };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTlsWhenAvailable);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
            //Email section end

            var user = new User
            {
                Email = request.Email,
                Username = "NewGardener",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = token,
                DateCreated = DateTime.Now,
                RoleId = 1
            };
            //_econtrol.SendEmail(token, user.Email);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //return RedirectToAction("Index");
            return RedirectToAction("Login");
            //Changed the page link. It goes to the Login now.
        }

        //[HttpPost]
        //public IActionResult SendEmail(string body, string temail)
        //{
        //    var email = new MimeMessage();
        //    //Sender for this service
        //    email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));

        //    //Recipient of email
        //    email.To.Add(MailboxAddress.Parse("miguelblanco.ureta@benilde.edu.ph"));

        //    //Subject and content of email
        //    email.Subject = "Test Email Subject";
        //    email.Body = new TextPart(TextFormat.Html) { Text = body };

        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
        //    smtp.Send(email);
        //    smtp.Disconnect(true);

        //    return RedirectToAction("Index", "Home");
        
        
        //New Security Check START////////////////////////////////////////////////////////
        public IActionResult ResetTokenIn()
        {
            return View();
        }
        [Route("/Account/")]
        public IActionResult ResetTokenIn(TokenInputModel token)
        {
            var tokenCheck = _context.Users.FirstOrDefault(t => t.VerificationToken == token.TokenIn).VerificationToken;
            if(tokenCheck ==null||tokenCheck!=token.TokenIn)
            {
                ViewData["ErrorMessage"] = "Token incorrect or does not exist.";
                return View("ResetTokenIn");
            }
            else if(_context.Users.FirstOrDefault(u => u.Email == token.EmailIn).Email!=_context.Users.FirstOrDefault(t => t.VerificationToken == token.TokenIn).Email)
            {
                ViewData["ErroMessage"] = "Token does not belong with given Email.";
                return View("ResetTokenIn");
            }
            else 
            {
                return View("ResetPassword");
            }
        }
        //New Security Check END/////////////////////////////////////////////////////////

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(ModelState.IsValid)
            {
                if (user == null)
                {
                    ViewData["LoginMessage"] = "The username or password is incorrect";
                    
                    return View("Login");
                }
                if (!verifyhashPassword(request.Password,user.PasswordHash,user.PasswordSalt))
                {
                    ViewData["LoginMessage"] = "The username or password is incorrect";
                    //return BadRequest("User does not exist");
                    return View("Login");
                }
                _hcontext.HttpContext.Session.SetInt32("logMemberID", user.UserId);
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
        //[AllowAnonymous]
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        [AllowAnonymous]
        [Route("aLogin")]
        public ActionResult aLogin()
        {
            _hcontext.HttpContext.Session.SetString("Session", "notlogged");
            return View();
        }
        //[AllowAnonymous]
        [HttpPost("aLogin")]
        public async Task<IActionResult> aLogin(AdminLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    ViewData["LoginMessage"] = "User does not exist";

                    return View("aLogin");
                }
                if (!verifyhashPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    ViewData["LoginMessage"] = "Invalid Login";
                    //return BadRequest("User does not exist");
                    return View("aLogin");
                }
                //Lead to Admin 
                if(user.RoleId == 6)
                {
                    _hcontext.HttpContext.Session.SetString("Session", "adminlogged");
                    _hcontext.HttpContext.Session.SetInt32("logUserID", user.UserId); 
                    return RedirectToAction("Index", "User");
                }
                if(user.RoleId==3)
                {
                    _hcontext.HttpContext.Session.SetString("Session", "diagmodlogged");
                    _hcontext.HttpContext.Session.SetInt32("logUserID", user.UserId);
                    return RedirectToAction("ModHome", "Home");
                }
                if (user.RoleId == 4 )
                {
                    _hcontext.HttpContext.Session.SetString("Session", "howtosmodlogged");
                    _hcontext.HttpContext.Session.SetInt32("logUserID", user.UserId);
                    return RedirectToAction("ModHome", "Home");
                }
                if (user.RoleId == 5)
                {
                    _hcontext.HttpContext.Session.SetString("Session", "forumsmodlogged");
                    _hcontext.HttpContext.Session.SetInt32("logUserID", user.UserId);
                    return RedirectToAction("ModHome", "Home");
                }

                ViewData["LoginMessage"] = "Invalid account login";
                return RedirectToAction("aLogin");             
            }
            else
            {
                ViewData["LoginMessage"] = "Login Error";
                return View("aLogin");
            }
        }
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult SendEmail(string body, string temail, string subj)
        {
            var email = new MimeMessage();
            //Sender for this service
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));

            //Recipient of email
            email.To.Add(MailboxAddress.Parse(temail));

            //Subject and content of email
            email.Subject = subj;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            return View("Login");
        }
        //[AllowAnonymous]
        public ActionResult adminAccess()
        {
            return View();
        }
        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> adminAccess(AdminCreationRequest request)
        {
            if (_context.Users.Any(u => u.RoleId == 6))
            {
                return BadRequest("Admin Already Assigned");
            }
            hashPassword(request.Password,
                out byte[] passwordHash, out byte[] passwordSalt);
            var token = CreateRandomToken();

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DateCreated = DateTime.Now,
                RoleId = 6
            };
            //_econtrol.SendEmail(token, user.Email);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //return RedirectToAction("Index");
            return RedirectToAction("aLogin");
        }

        public ActionResult termsAndConditions()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestModel request)
        {
            var forgotUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(forgotUser==null)
            {
                ViewData["ErrorMessage"] = "User does not exist";
                return View("ForgotPassword");
            }
            else
            {
                //SendEmail("Reset Password Token: " + token, email, "Reset Password Request");
                var nemail = new MimeMessage();
                var token = CreateRandomToken();
                nemail.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                nemail.To.Add(MailboxAddress.Parse(request.Email));
                nemail.Subject = "Forgot Password Request";
                nemail.Body = new TextPart(TextFormat.Html) { Text = "Link to Password reset with token: "+ token };

                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTlsWhenAvailable);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(nemail);
                smtp.Disconnect(true);

                //Assigning generated token to user with email
                User tUser = _context.Users.Where(u => u.Email == request.Email).SingleOrDefault();
                tUser.VerificationToken = token.ToString();
                tUser.ResetTokenExpires = DateTime.Now.AddHours(1);
                _context.Users.Update(tUser);
                _context.SaveChanges();
                _hcontext.HttpContext.Session.SetString("ResetToken", token);

                //Uncomment below to revert back to previous reset password method
                //return View("ResetPassword");

                //New Reset password process
                return View("ResetTokenIn");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            request.Token = _hcontext.HttpContext.Session.GetString("ResetToken");
            var forgotUser = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == request.Token);
            if (forgotUser == null)
            {
                ViewData["ErrorMessage"] = "User does not exist";
                return View("ResetPassword");
            }
            else if(forgotUser.ResetTokenExpires<DateTime.Now)
            {
                ViewData["ErrorMessage"] = "Reset Request Expired";
                return View("ResetPassword");
            }
            else if(_hcontext.HttpContext.Session.GetString("ResetToken")!=forgotUser.VerificationToken)
            {
                ViewData["ErrorMessage"] = "Reset Token Error";
                return View("ResetPassword");
            }
            else
            {
                hashPassword(request.Password, out byte[] passHash, out byte[] passSalt);
                forgotUser.PasswordHash = passHash;
                forgotUser.PasswordSalt = passSalt;
                forgotUser.ResetTokenExpires = null;
                forgotUser.VerificationToken = null;
                ViewData["LoginUpdate"] = "Password Updated";
                await _context.SaveChangesAsync();
            }
            return View("Login");
        }
    }
}
