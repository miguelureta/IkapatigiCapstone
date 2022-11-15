using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace IkapatigiCapstone.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _hcontext;
        public UserController(ApplicationDbContext context, IWebHostEnvironment webHost, IHttpContextAccessor hcontext)
        {
            _context = context;
            _webHostEnvironment = webHost;
            _hcontext = hcontext;
        }

        public IEnumerable<User> GetUserList { get; set; }
        // GET: User
        //[Authorize]
        [AutoValidateAntiforgeryToken]
        public ActionResult Index()
        {
            string? sesh = _hcontext.HttpContext.Session.GetString("Session");
            if(sesh==null||!sesh.Equals("adminlogged"))
            {
                return BadRequest("Invalid View");
            }
            var userl = _context.Users.ToList();
            return View("Index",userl);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<IActionResult> Create(ModeratorCreationModel model)
        {
            if(_context.Users.Any(u=>u.Username==model.Username))
            {
                ViewBag["ModError"] = "Moderator Username In Use";
                return View("Create");
            }
            hashPassword(model.Password,
                out byte[] passwordHash, out byte[] passwordSalt);

            var mod = new User
            {
                Username=model.Username,
                PasswordHash=passwordHash,
                PasswordSalt=passwordSalt,
                RoleId=model.RoleId,
                DateCreated=DateTime.Now
            };

            _context.Users.Add(mod);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
            
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var acc = _context.Users.Where(i => i.UserId == id).SingleOrDefault();

            _context.Users.Remove(acc);
            _context.SaveChanges();

            return RedirectToAction("Index","User");
        }

        private void hashPassword(string pw, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pw));
            }
        }

    }
}
