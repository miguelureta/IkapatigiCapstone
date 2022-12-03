using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Data;
using IkapatigiCapstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IkapatigiCapstone.Controllers
{
    public class HowToController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _hcontext;
        //private readonly IWebHostEnvironment _webHostEnvironment;//This is for the GetStatusList()
        public HowToController(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor hcontext)
        {
            _context = context;
            _config = config;
            _hcontext = hcontext;
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
            if (_hcontext.HttpContext.Session.GetString("Session").Equals("howtosmodlogged") || _hcontext.HttpContext.Session.GetString("Session").Equals("adminlogged"))
            {
                var list = _context.HowTos.ToList();
                return View(list);
            }
            else
            {
                return Content("Access Denied. This page is not available for your role.");
            }
        }

        public IActionResult Create()
        {
            if (_hcontext.HttpContext.Session.GetString("Session").Equals("howtosmodlogged"))
            {
                return View();


                        }
            else
            {
                return Content("Access Denied. This page is not available for your role.");
            }

        }
        
        //Added function to send MembersOnly HowTo articles to Users with Member Role = 1
        [HttpPost]
        public IActionResult Create(HowTo record)
        {
            //var howto = new HowTo();
            //howto.Title = record.Title;
            //howto.Description = record.Description;
            //howto.LikeCount = record.LikeCount;
            //howto.DislikeCount = record.DislikeCount;
            //howto.IsPublic = record.IsPublic;
            //howto.UserID = record.UserID;
            //howto.StatusID = record.StatusID;
            //howto.PictureCollectionFromID = record.PictureCollectionFromID;
            //howto.DateCreated = DateTime.Now;
            //howto.ArticleBody = record.ArticleBody;
            //var memberList = _context.Users.Where(u => u.RoleId == 1).ToList();

            //_context.HowTos.Add(howto);
            //_context.SaveChanges();
            //return RedirectToAction("Index");

            //Holy smokes it works
            //if (howto.IsPublic == Availability.Members)
            //{
            //    foreach (var member in memberList)
            //    {
            //        var email = new MimeMessage();
            //        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            //        email.To.Add(MailboxAddress.Parse(member.Email));
            //        email.Subject = "New Members Only Content";
            //        email.Body = new TextPart(TextFormat.Html) { Text = howto.Title };

            //        using var smtp = new SmtpClient();
            //        //Commented smtp line is for sending emails when deployed in Capstone Repo
            //        //smtp.Connect(_config.GetSection("EmailHost").Value, 25, SecureSocketOptions.StartTlsWhenAvailable);
            //        smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTlsWhenAvailable);
            //        smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            //        smtp.Send(email);
            //        smtp.Disconnect(true);
            //    }
            //}

           
                try
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
                    //if (howto.IsPublic == Availability.Members)
                    //{
                    //    foreach (var member in memberList)
                    //    {
                    //        var email = new MimeMessage();
                    //        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                    //        email.To.Add(MailboxAddress.Parse(member.Email));
                    //        email.Subject = "New Members Only Content";
                    //        email.Body = new TextPart(TextFormat.Html) { Text = howto.Title };

                    //        using var smtp = new SmtpClient();
                    //        //Commented smtp line is for sending emails when deployed in Capstone Repo
                    //        //smtp.Connect(_config.GetSection("EmailHost").Value, 25, SecureSocketOptions.StartTlsWhenAvailable);
                    //        smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTlsWhenAvailable);
                    //        smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                    //        smtp.Send(email);
                    //        smtp.Disconnect(true);
                    //    }
                    //}

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return Content("Unable to save changes. Please check that you don't have any empty boxes. ");


                }




         

            //TempData["SuccessMessage"] = "New How To named: " + record.Title + "Created Successfully";

        }


        public IActionResult Edit(int? id)
        {
            if (_hcontext.HttpContext.Session.GetString("Session").Equals("howtosmodlogged"))
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
            else
            {
                return Content("Access Denied. This page is not available for your role.");
            }
        }


        [HttpPost]
        public IActionResult Edit(int? id, HowTo record)
        {
            try
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

            catch (Exception ex)
            {
                return Content("Unable to save changes. Please check that you don't have any empty boxes. ");


            }

            //try
            //{
            //    var howto = _context.HowTos.Where(i => i.HowTosID == id).SingleOrDefault();
            //    howto.Title = record.Title;
            //    howto.Description = record.Description;
            //    howto.LikeCount = record.LikeCount;
            //    howto.DislikeCount = record.DislikeCount;
            //    howto.IsPublic = record.IsPublic;
            //    howto.UserID = record.UserID;
            //    howto.StatusID = record.StatusID;
            //    howto.PictureCollectionFromID = record.PictureCollectionFromID;
            //    howto.DateUpdated = DateTime.Now;
            //    howto.ArticleBody = record.ArticleBody;


            //    _context.HowTos.Update(howto);
            //    _context.SaveChanges();

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    ModelState.AddModelError("", "Unable to edit. Please check that there are no empty boxes.");
            //}



        }

        public IActionResult Delete(int? id)
        {
            if (_hcontext.HttpContext.Session.GetString("Session").Equals("howtosmodlogged"))
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
                var article = _context.HowTos.Where(h => h.HowTosID == id).SingleOrDefault();


                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(article.User.Email));
                email.Subject = "Article Deleted";
                email.Body = new TextPart(TextFormat.Html) { Text = howto.Title + "was deleted! Contact admins to appeal." };

                using var smtp = new SmtpClient();
                //Commented smtp line is for sending emails when deployed in Capstone Repo
                //smtp.Connect(_config.GetSection("EmailHost").Value, 25, SecureSocketOptions.StartTlsWhenAvailable);
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTlsWhenAvailable);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                _context.HowTos.Remove(howto);
                _context.SaveChanges();

          
                return RedirectToAction("Index");

            }
            else
            {
                return Content("Access Denied. This page is not available for your role.");
            }
        }


        //Delete for checkbox
        [HttpPost]
        public ActionResult GroupDelete(IEnumerable<int> id)
        {
            if (_hcontext.HttpContext.Session.GetString("Session").Equals("howtosmodlogged"))
            {
       

                var howto = _context.HowTos.Where(i => id.Contains(i.HowTosID)).ToList();
                foreach(HowTo h in howto)
                {
                    _context.HowTos.Remove(h);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            else
            {
                return Content("Access Denied. This page is not available for your role.");
            }
        }

        //public IActionResult CheckboxDeleteHowTo(List<HowTo> howto)
        //{
        //    List<HowTo> howto = new List<HowTo>();
        //    foreach (var item in howto)
        //    {
        //        if (item.Emps.Selected)
        //        {
        //            var howto = _context.HowTos.Where(i => i.HowTosID == id).SingleOrDefault();
        //            howto.Add(selected)
        //        }
        //    }
        //}

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


        public IActionResult MemberIndex()
        {
            var list = _context.HowTos.ToList();
            return View(list);
        }

        public ActionResult MemberDetails(int? id)
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
            return View("MemberDetails",howto);
        }



        public IActionResult NonMemberIndex()
        {
            var list = _context.HowTos.ToList();
            return View(list);
        }

        public ActionResult NonMemberDetails(int? id)
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
            return View("NonMemberDetails", howto);
        }
    }
}
