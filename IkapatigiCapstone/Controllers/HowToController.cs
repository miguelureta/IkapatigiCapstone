using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Data;
using IkapatigiCapstone.Models;

namespace IkapatigiCapstone.Controllers
{
    public class HowToController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HowToController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var list = _context.HowTos.ToList();
            return View(list);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(HowTo record)
        {
            var howto = new HowTo();
            howto.Title = record.Title; 
            howto.Description = record.Description;
            howto.LikeCount = record.LikeCount;
            howto.DislikeCount = record.DislikeCount;
            howto.IsPublic = record.IsPublic;
            howto.UserID = record.UserID;
            howto.StatusID = record.StatusID;
            howto.PictureCollectionFromID = record.PictureCollectionFromID;
            howto.DateCreated = DateTime.Now;
            howto.ArticleBody = record.ArticleBody;


            _context.HowTos.Add(howto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var howto = _context.HowTos.Where(i => i.HowTosID == id).SingleOrDefault();
            if (howto == null)
            {
                return RedirectToAction("Index");
            }

            return View(howto);
        }


        [HttpPost]
        public IActionResult Edit(int? id, HowTo record)
        {
            var howto = _context.HowTos.Where(i => i.HowTosID == id).SingleOrDefault();
            howto.Title = record.Title;
            howto.Description = record.Description;
            howto.LikeCount = record.LikeCount;
            howto.DislikeCount = record.DislikeCount;
            howto.IsPublic = record.IsPublic;
            howto.UserID = record.UserID;
            howto.StatusID = record.StatusID;
            howto.PictureCollectionFromID = record.PictureCollectionFromID;
            howto.DateUpdated = DateTime.Now;
            howto.ArticleBody = record.ArticleBody;


            _context.HowTos.Update(howto);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var howto = _context.HowTos.Where(i => i.HowTosID == id).SingleOrDefault();
            if (howto == null)
            {
                return RedirectToAction("Index");
            }

            _context.HowTos.Remove(howto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        //This is Add Details in the Index page, just fixed.

        public ActionResult Details(int? id)
        {
            HowTo page = new HowTo();
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            page = _context.HowTos.Find();
            if(page == null)
            {
                return RedirectToAction("Index");
            }
            return View(page);
        }


        //Previous WRONG details page>>>
        public IActionResult Article()
        {
            var list = _context.HowTos.ToList();
            return View(list);
        }


        //

    }
}
