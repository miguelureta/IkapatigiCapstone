using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Data;
using Microsoft.EntityFrameworkCore;
using IkapatigiCapstone.ViewModel;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Identity;

namespace IkapatigiCapstone.Controllers
{
    public class HeadAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        public HeadAdminController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET: Users 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync();
            return View();
        }

        public IActionResult LoadMembers()
        {
            return View();
        }

        public IActionResult LoadApplicants()
        {
            return View();
        }

        public IActionResult CreateMod()
        {
            return View(new RegisterModViewModel());
        }

        public async Task<IActionResult> AddModerator(RegisterModViewModel model)
        {
            if(ModelState.IsValid==true)
            {
                Users newUser = new Users();
                newUser.Username = model.Username;
                newUser.Password = model.Password;
                newUser.DateCreated = DateTime.Now;
                newUser.RoleId = model.RoleId;
                //1 for DiagnosticMod, 2 for HowTosMod, 3 for ForumMod

                //await _userManager.CreateAsync(newUser, model.Password);

                return RedirectToAction("Index", "HeadAdmin");
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult ViewModerators()
        {
            return View();
        }
    }
}
