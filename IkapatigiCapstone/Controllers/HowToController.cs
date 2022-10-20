using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Data;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IkapatigiCapstone.Controllers
{
    public class HowToController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HowToController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Status> GetStatus { get; set; }

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


        //This is Add Details in the Index page.

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            HowTo howto = _context.HowTos.Find(id);
            if (howto == null)
            {
                return RedirectToAction("Index");
            }
            return View(howto);
        }


        //This is Banned in the Index Page. -- Currently can't change the StatusID

        public IActionResult Banned(int? id)
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

        public IActionResult Banned(int? id, HowTo record)
        {
            var howto = _context.HowTos.Where(i => i.HowTosID == id).SingleOrDefault();
            howto.StatusID = record.StatusID;
            


            _context.HowTos.Update(howto);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }


        //Get Foreign Key Status

        //private List<SelectListItem> GetStatusList()
        //{
        //    var lstStatus = new List<SelectListItem>();
        //    lstStatus = _context.Statuses.Select(ct => new SelectListItem()
        //    {
        //        ValueTask = ct.StatusId.ToString(),
        //        TextReader = ct.StatusType
        //    }).ToList();

        //    var dmyItem = new SelectListItem()
        //    {
        //        Value = null;
        //        Text = "Select Status"
        //    };

        //    return lstStatus;
        //}

    }
}
