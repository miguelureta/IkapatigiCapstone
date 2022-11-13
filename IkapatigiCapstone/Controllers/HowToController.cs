using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Data;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace IkapatigiCapstone.Controllers
{
    public class HowToController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        //private readonly IWebHostEnvironment _webHostEnvironment;//This is for the GetStatusList()
        public HowToController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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
        
        //Added function to send MembersOnly HowTo articles to Users with Member Role = 1
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
            var memberList = _context.Users.Where(u => u.RoleId == 1).ToList();
            
            _context.HowTos.Add(howto);
            _context.SaveChanges();

            //Holy smokes it works
            if (howto.IsPublic == Availability.Members)
            {
                foreach (var member in memberList)
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                    email.To.Add(MailboxAddress.Parse(member.Email));
                    email.Subject = "New Members Only Content";
                    email.Body = new TextPart(TextFormat.Html) { Text = howto.Title };

                    using var smtp = new SmtpClient();
                    //Commented smtp line is for sending emails when deployed in Capstone Repo
                    //smtp.Connect(_config.GetSection("EmailHost").Value, 25, SecureSocketOptions.StartTlsWhenAvailable);
                    smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTlsWhenAvailable);
                    smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }

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
                //ViewBag.StatusId = GetStatusList(id);
                return BadRequest("Screen for retrieving banned list");
            }
            

            return View(howto);
        }

        //[HttpPost]
        //public IActionResult Banned(int? id, HowTo article)
        //{
        //    var howto = _context.HowTos.Where(i => i.StatusID == article.StatusID).SingleOrDefault();
        //    UpdateHowToStatusModel mod = new UpdateHowToStatusModel();
        //    if(howto != null)
        //    {
        //        howto.StatusID = model.StatusId;
        //        howto.Status = model.StatusType;
        //    }

        //    _context.HowTos.Update(howto);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        //MICO'S ATTEMPT TO EDIT STATUS END
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
