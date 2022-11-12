using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;

namespace IkapatigiCapstone.Controllers
{
    public class PlantDiseaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantDiseaseController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var list = _context.PlantDiseases.ToList();
            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(PlantDisease record)
        {
            var plantdisease = new PlantDisease()
            {
                DiseaseName = record.DiseaseName,
                ImageOfDisease = record.ImageOfDisease,
                TagId = record.TagId,
                CureId = record.CureId,
                UserId = record.UserId
            };

            _context.PlantDiseases.Add(plantdisease);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var plantdisease = _context.PlantDiseases.Where(i => i.PlantDiseaseId == id).SingleOrDefault();
            if (plantdisease == null)
            {
                return RedirectToAction("Index");
            }

            return View(plantdisease);
        }


        [HttpPost]
        public IActionResult Edit(int? id, PlantDisease record)
        {
            var plantdisease = _context.PlantDiseases.Where(i => i.PlantDiseaseId == id).SingleOrDefault();
            plantdisease.DiseaseName = record.DiseaseName;
            plantdisease.ImageOfDisease = record.ImageOfDisease;
            plantdisease.TagId = record.TagId;


            _context.PlantDiseases.Update(plantdisease);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var plantdisease = _context.PlantDiseases.Where(i => i.PlantDiseaseId == id).SingleOrDefault();
            if (plantdisease == null)
            {
                return RedirectToAction("Index");
            }

            _context.PlantDiseases.Remove(plantdisease);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}