using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;

namespace IkapatigiCapstone.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Tag.ToList();
            return View(list);
        }


        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag record)
        {
            var tag = new Tag()
            {
                TagName = record.TagName,
                UserID = record.UserID
            };

            _context.Tag.Add(tag);  
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var tag = _context.Tag.Where(i => i.TagID == id).SingleOrDefault();
            if (tag == null)
            {
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Tag record)
        {
            var tag = _context.Tag.Where(i => i.TagID == id).SingleOrDefault();
            tag.TagName = record.TagName;
            tag.UserID = record.UserID;


            _context.Tag.Update(tag);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var tag = _context.Tag.Where(i => i.TagID == id).SingleOrDefault();
            if (tag == null)
            {
                return RedirectToAction("Index");
            }

            _context.Tag.Remove(tag);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}