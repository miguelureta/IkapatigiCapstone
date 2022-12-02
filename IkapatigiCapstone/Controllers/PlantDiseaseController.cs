using Microsoft.AspNetCore.Mvc;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IkapatigiCapstone.Controllers
{
    public class PlantDiseaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webhost;
        private readonly IHttpContextAccessor _hcontext;
        public PlantDiseaseController(ApplicationDbContext context, IWebHostEnvironment webHost, IHttpContextAccessor hContext)
        {
            _context = context;
            _webhost = webHost;
            _hcontext = hContext;
        }
        public IEnumerable<Tag> GetTag { get; set; }
        public IEnumerable<Cure> GetCure { get; set; }

        public IActionResult Index()
        {
            var list = _context.PlantDiseases.ToList();
            //var list = new PlantDiseaseImageView();
            //list = _context.PlantDiseases.ToList();
            //list.Images = _context.Image.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CureId = GetCureList();
            ViewBag.TagId = GetTagList();
            PlantDiseaseImage pDI = new PlantDiseaseImage();
            return View(pDI);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlantDiseaseImage record)
        {

            string filePath = null;
            string fileName = null;
            byte[] fileBytes = null;

            if(record.PdImage==null)
            {
                return View();
            }
            
            string uploadFolder = Path.Combine(_webhost.WebRootPath, "images");
            fileName = Guid.NewGuid().ToString() + "_" + record.PdImage.FileName;
            filePath = Path.Combine(uploadFolder, fileName);
            using(var fileStream=new FileStream(filePath, FileMode.Create))
            {
                await record.PdImage.CopyToAsync(fileStream);
                fileStream.CopyTo(fileStream);
                
                fileStream.Close();
            }
            using (var ms = new MemoryStream())
            {
                record.PdImage.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            var plantdisease = new PlantDisease()
            {
                DiseaseName = record.DiseaseName,
                ImageOfDisease = fileBytes,//This is not really needed
                TagId = record.TagId,
                CureId = record.CureId,
                //UserId = record.UserId
            };
            _context.PlantDiseases.Add(plantdisease);
            _context.SaveChanges();
            var dImage = new Image
            {
                PlantDiseaseID = plantdisease.PlantDiseaseId,
                ImageName = fileName,
                UserID = (int)_hcontext.HttpContext.Session.GetInt32("logUserID")
            };
            _context.Image.Add(dImage);
            _context.SaveChanges();



            return RedirectToAction("Index");
        }
        //Method for creating url pointing to location of image in project
        
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
    }
}