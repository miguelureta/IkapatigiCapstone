using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;

namespace IkapatigiCapstone.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _hcontext;
        public TagController(ApplicationDbContext context, IHttpContextAccessor hcontext)
        {
            _context = context;
            _hcontext = hcontext;
        }

        public IActionResult Index()
        {
            if (_hcontext.HttpContext.Session.GetString("Session").Equals("diagmodlogged") || _hcontext.HttpContext.Session.GetString("Session").Equals("adminlogged"))
            {
                var list = _context.Tags.ToList();
                return View(list);
            }
            else
            {
                return Content("Access Denied. This page is not available for your role.");
            }
        }


        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag record)
        {
            try
            {
                var tag = new Tag()
                {
                    TagName = record.TagName,
                    UserId = record.UserId
                };

                _context.Tags.Add(tag);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Unable to save changes. Please check that you don't have any empty boxes. ");


            }
     
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var tag = _context.Tags.Where(i => i.TagId == id).SingleOrDefault();
            if (tag == null)
            {
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Tag record)
        {
            try
            {
                var tag = _context.Tags.Where(i => i.TagId == id).SingleOrDefault();
                tag.TagName = record.TagName;
                tag.UserId = record.UserId;


                _context.Tags.Update(tag);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Unable to save changes. Please check that you don't have any empty boxes. ");


            }
        

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var tag = _context.Tags.Where(i => i.TagId == id).SingleOrDefault();
            if (tag == null)
            {
                return RedirectToAction("Index");
            }

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}