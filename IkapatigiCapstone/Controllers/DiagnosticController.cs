using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;

namespace IkapatigiCapstone.Controllers
{
    public class DiagnosticController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DiagnosticController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            this.webHostEnvironment = webHost;
        }
        public IEnumerable<Cure> GetCure { get; set; }
        public IEnumerable<Status> GetStatus { get; set; }
        public IEnumerable<Tag> GetTag { get; set; }
        public IEnumerable<PlantDisease> GetDisease { get; set; }

       
        public IActionResult Index()
        {
            var dvm = new DiagnosticViewModel();
            //List<DiagnosticViewModel> list = new List<DiagnosticViewModel>();
            //var diag = _context.Diagnostics.Include(a => a.CureId).ThenInclude<Tag => Diagnostic.Tag>;
            //var diag = _context.Diagnostics.ToList();
            //foreach(var Diagnostic in diag)
            //{
            //Diagnostic d = new Diagnostic();
            //DiagnosticViewModel dvm = new DiagnosticViewModel();
            //var diagCureItem = _context.Cures.Where(x => x.CureId == d.CureId);
            //var diagDiseaseItem = _context.PlantDiseases.Where(x => x.PlantDiseaseId == d.PlantDiseaseId);
            //var diagStatusItem = _context.Statuses.Where(x => x.StatusId == d.StatusId);
            //var diagTagItem = _context.Tags.Where(x => x.TagId == d.TagId);
            dvm.CureList = _context.Cures.ToList();
            dvm.DiseaseList = _context.PlantDiseases.ToList();
            dvm.StatusList = _context.Statuses.ToList();
            dvm.TagsList = _context.Tags.ToList();
            dvm.Diagnostic = _context.Diagnostics.ToList();
            //dvm.CureList = diagCureItem;
            //dvm.DiseaseList = diagDiseaseItem;
            //dvm.StatusList = diagStatusItem;
            //dvm.TagsList = diagTagItem;
            //    list.Add(dvm);
            //}
            return View(dvm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CureId = GetCureList();
            ViewBag.TagId = GetTagList();
            ViewBag.StatusId = GetStatusList();
            ViewBag.PlantDiseaseId = GetPlantDiseasesList();
            Diagnostic diagInput = new Diagnostic();
            return View(diagInput);
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

        //For uploading image to application folders
        private string UploadedFile(Diagnostic diag)
        {
            string uniqueImageName = null;

            if(diag.DisplayImage !=null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + diag.DisplayImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueImageName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    diag.DisplayImage.CopyTo(fileStream);
                }
            }
            return uniqueImageName;
        }

        private List<SelectListItem> GetTagList()
        {
            var lstTags = new List<SelectListItem>();
            lstTags = _context.Tags.Select(ct => new SelectListItem()
            {
                Value = ct.TagId.ToString(),
                Text = ct.TagName
            }).ToList();

            var dmyItem = new SelectListItem()
            {
                Value = null,
                Text = "Select Tag"
            };

            lstTags.Insert(0, dmyItem);

            return lstTags;
        }

        private List<SelectListItem> GetCureList()
        {
            var lstCures = new List<SelectListItem>();
            lstCures = _context.Cures.Select(ct => new SelectListItem()
            {
                Value = ct.CureId.ToString(),
                Text = ct.CureName
            }).ToList();

            var dmyItem = new SelectListItem()
            {
                Value = null,
                Text = "Select Cure"
            };

            lstCures.Insert(0, dmyItem);

            return lstCures;
        }

        private List<SelectListItem> GetStatusList()
        {
            var lstStatus = new List<SelectListItem>();
            lstStatus = _context.Statuses.Select(ct => new SelectListItem()
            {
                Value = ct.StatusId.ToString(),
                Text = ct.StatusType
            }).ToList();

            var dmyItem = new SelectListItem()
            {
                Value = null,
                Text = "Select Status"
            };

            lstStatus.Insert(0, dmyItem);

            return lstStatus;
        }

        private List<SelectListItem> GetPlantDiseasesList()
        {
            var lstPlantDiseases = new List<SelectListItem>();
            lstPlantDiseases = _context.PlantDiseases.Select(ct => new SelectListItem()
            {
                Value = ct.PlantDiseaseId.ToString(),
                Text = ct.DiseaseName
            }).ToList();

            var dmyItem = new SelectListItem()
            {
                Value = null,
                Text = "Select Disease"
            };

            lstPlantDiseases.Insert(0, dmyItem);

            return lstPlantDiseases;
        }
    }
}