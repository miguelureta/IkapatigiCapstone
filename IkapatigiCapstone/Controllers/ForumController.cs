using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;
namespace IkapatigiCapstone.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ForumController
        public ActionResult Index()
        {
            var list = _context.Forums.ToList();
            return View(list);
        }

        // GET: ForumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Forum _forum/*, User _user*/)
        {
            var forum = new Forum();
            try
            {
                forum.Title = _forum.Title;
                forum.Description = _forum.Description;
                forum.ImageUrl = _forum.ImageUrl;
                forum.Created = DateTime.Now;
                //forum.UserId = _user.UserId;
                _context.Forums.Add(forum);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForumController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var forum = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();
            if (forum == null)
            {
                return RedirectToAction("Index");
            }

            return View(forum);
        }

        // POST: ForumController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Forum post)
        {
            var newp = new Forum();
            newp.Title = post.Title;
            newp.Description = post.Description;
            newp.ImageUrl = post.ImageUrl;
            newp.Created = DateTime.Now;
            _context.Forums.Update(newp);
            _context.SaveChanges();
            return RedirectToAction("Index");
            //try
            //{
            //    _context.Forums.Update(newp);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: ForumController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ForumController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Forum");
            }

            var post = _context.Forums.Where(i => i.ForumId == id).SingleOrDefault();
            if (post == null)
            {
                return RedirectToAction("Index", "Forum");
            }

            _context.Forums.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Forum");
        }
    }
}
