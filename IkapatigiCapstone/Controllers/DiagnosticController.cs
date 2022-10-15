using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;

namespace IkapatigiCapstone.Controllers
{
    public class DiagnosticController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Diagnostics.ToList();
            return View(list);
        }


        public IActionResult CreateHowTo()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateHowTo(Diagnostic record)
        {
            var diagnostic = new Diagnostic()
            {
                PictureCollectionFromId = record.PictureCollectionFromId,
                CureId = record.CureId,
                StatusId = record.StatusId,
                TagId = record.TagId,
                PlantDiseaseId = record.PlantDiseaseId
            };

            _context.Diagnostics.Add(diagnostic);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var diagnostic = _context.Diagnostics.Where(i => i.DiagnosticsId == id).SingleOrDefault();
            if (diagnostic == null)
            {
                return RedirectToAction("Index");
            }

            return View(diagnostic);
        }


        [HttpPost]

        public IActionResult Edit(int? id, Diagnostic record)
        {
            var diagnostic = _context.Diagnostics.Where(i => i.DiagnosticsId == id).SingleOrDefault();
            diagnostic.PictureCollectionFromId = record.PictureCollectionFromId;
            diagnostic.CureId = record.CureId;
            diagnostic.StatusId = record.StatusId;
            diagnostic.TagId = record.TagId;
            diagnostic.PlantDiseaseId = record.PlantDiseaseId;


            _context.Diagnostics.Update(diagnostic);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var diagnostic = _context.Diagnostics.Where(i => i.DiagnosticsId == id).SingleOrDefault();
            if (diagnostic == null)
            {
                return RedirectToAction("Index");
            }

            _context.Diagnostics.Remove(diagnostic);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}