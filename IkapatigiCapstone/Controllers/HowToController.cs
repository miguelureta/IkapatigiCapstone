using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Data;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IkapatigiCapstone.Controllers
{
    public class HowToController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;//This is for the GetStatusList()
        public HowToController(ApplicationDbContext context)
        {
            _context = context;

        }
        /* Uncomment this Controller constructor and comment the above constructor to
         * work with GetStatusList()
        public HowToController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            this.webHostEnvironment = webHost;
        }
        */
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

        /*COMMENTING THIS OUT SO YOU STILL HAVE YOUR CODE
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
        */
        //MICO'S ATTEMPT TO EDIT STATUS START
        [HttpGet]
        public IActionResult Banned(int id)
        {
            var howto = _context.HowTos.Where(i => i.HowTosID == id).SingleOrDefault();
            if (howto == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //Make Banned ViewPage with just the dropdownlist
                //to change the Status of the selected article
                ViewBag.StatusId = GetStatusList(id);
            }
            

            return View(howto);
        }

        [HttpPost]
        public IActionResult Banned(int? id, HowTo article)
        {
            var howto = _context.HowTos.Where(i => i.StatusID == article.StatusID).SingleOrDefault();
            UpdateHowToStatusModel mod = new UpdateHowToStatusModel();
            if(howto != null)
            {
                howto.StatusID = model.StatusId;
                howto.Status = model.StatusType;
            }

            _context.HowTos.Update(howto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //MICO'S ATTEMPT TO EDIT STATUS END
        //Get Foreign Key Status

        //Should assign currect status of article and other selectable statuses from database
        //Needs constructor with iwebhostenvironment to work
        private List<SelectListItem> GetStatusList(int id)
        {
            var lstStatus = new List<SelectListItem>();
            lstStatus = _context.Statuses.Select(ct => new SelectListItem()
            {
                Value = ct.StatusId.ToString(),
                Text = ct.StatusType
            }).ToList();
            var article = _context.HowTos.Where( s => s.HowTosID == id).ToString()
            var listItem = new SelectListItem()
            {
                
                Value = ,
                Text = 
            };
            lstStatus.Insert(0, listItem);
            return lstStatus;
        }

    }
}
