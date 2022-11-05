using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;

namespace IkapatigiCapstone.Controllers
{
    public class CureController : Controller
    {
        private readonly ApplicationDbContextOut _context;

        public CureController(ApplicationDbContextOut context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Cures.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cure record)
        {
            var cure = new Cure()
            {
                CureName = record.CureName,
                Srp = record.Srp,
                UserId = record.UserId
    
             };

            _context.Cures.Add(cure);
            _context.SaveChanges();

            return RedirectToAction("Index");
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
            var cure = _context.Cures.Where(i => i.CureId == id).SingleOrDefault();
            cure.CureName = record.CureName;
            cure.Srp = record.Srp;
            cure.UserId = record.UserId;


            _context.Cures.Update(cure);
            _context.SaveChanges();

            return RedirectToAction("Index");

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

            _context.Cures.Remove(cure);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}