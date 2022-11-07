using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;

namespace IkapatigiCapstone.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            this.webHostEnvironment = webHost;
        }

        public IEnumerable<User> GetUserList { get; set; }
        // GET: User
        public ActionResult Index()
        {
            var userl = new UserManagementViewModel();
            userl.Status = _context.Statuses.ToList().ToString();
            userl.User = _context.Users.ToList();
            return View(userl);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
