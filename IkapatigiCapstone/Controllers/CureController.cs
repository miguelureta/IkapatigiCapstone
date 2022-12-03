using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;
using Org.BouncyCastle.Crypto.Tls;

namespace IkapatigiCapstone.Controllers
{
    public class CureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _hcontext;

        public CureController(ApplicationDbContext context, IHttpContextAccessor hcontext)
        {
            _context = context;
            _hcontext = hcontext;
        }

        public IActionResult Index()
        {
           

            if (_hcontext.HttpContext.Session.GetString("Session").Equals("diagmodlogged") || _hcontext.HttpContext.Session.GetString("Session").Equals("adminlogged"))
            {
                var list = _context.Cures.ToList();
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
        public IActionResult Create(Cure record)
        {
            //if (record.CureName.Equals(" ") && record.Srp.Equals(" ") )
            //{
            //    return RedirectToAction("Index");


            //}

            try {

                var cure = new Cure()
                {
                    CureName = record.CureName,
                    Srp = record.Srp,
                    UserId = record.UserId
                };

                _context.Cures.Add(cure);
                _context.SaveChanges();

                var audIn = new Audit()
                {
                    RoleId = 3,
                    Reason = "Created " + cure.CureName,
                    DateTime = DateTime.Now,
                    UserId = _hcontext.HttpContext.Session.GetInt32("logUserID")
                };
                _context.Audits.Add(audIn);
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

            var cure = _context.Cures.Where(i => i.CureId == id).SingleOrDefault();
            if (cure == null)
            {
                return RedirectToAction("Index");
            }

            return View(cure);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Cure record)
        {
            try
            {
                var cure = _context.Cures.Where(i => i.CureId == id).SingleOrDefault();
                cure.CureName = record.CureName;
                cure.Srp = record.Srp;
                cure.UserId = record.UserId;


                _context.Cures.Update(cure);
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

            var cure = _context.Cures.Where(i => i.CureId == id).SingleOrDefault();
            if (cure == null)
            {
                return RedirectToAction("Index");
            }

            var audIn = new Audit()
            {
                RoleId = 3,
                Reason = "Deleted " + cure.CureName,
                DateTime = DateTime.Now,
                UserId = _hcontext.HttpContext.Session.GetInt32("logUserID")
            };
            _context.Audits.Add(audIn);
            _context.SaveChanges();

            _context.Cures.Remove(cure);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}