using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Identity;
using IkapatigiCapstone.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IkapatigiCapstone.Controllers
{
    [AllowAnonymous, Route("Account")]
    public class AccountController : Controller
    {
        //private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        //private readonly SignInManager<User> _signInManager;
        public AccountController(/*UserManager<User> userManager, */ApplicationDbContext context/*, SignInManager<User> signInManager*/)
        {
            _context = context;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties{RedirectUri = Url.Action("GoogleResponse")};
            return Challenge(properties, Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme);
        }

        [ValidateAntiForgeryToken]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //User newUser = new User();
            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);
            //newUser.Email = result.Principal.Identities.FirstOrDefault()
            //    .Claims.Select(claim => new
            //{
            //    claim.Value
            //}).ToString();

            //await userManager.CreateAsync(newUser, );
            /*originally*/ 
            //return RedirectToAction("RedirectToLanding",claims);
        }
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
            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User Registered!");
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
                return Ok("User Signed In");
                //return RedirectToAction("Index", "Home");
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
                return BadRequest("User is not verified");
            }
            user.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Home", "Index");
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
    }
}
