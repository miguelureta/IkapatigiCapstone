using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Identity;
using IkapatigiCapstone.Data;
using System.Security.Cryptography;
using System.Text;

namespace IkapatigiCapstone.Controllers
{
    [AllowAnonymous, Route("account")]
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

        [HttpPost]
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

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User u)
        {
            var user = new User();
            user.Email = u.Email;
            user.Password = u.Password;
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //Code for hashing password
        private string hashPassword(string pw)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(pw);
            var hash = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hash);
        }

        private bool checkPassword(string pw)
        {
            string storedpass="";
            if(pw == hashPassword(storedpass))
            {
                return true;
            }
            return false;
        }
        //public IActionResult RedirectToLanding()
        //{
        //    return RedirectToAction("Index","Home");
        //}
    }
}
