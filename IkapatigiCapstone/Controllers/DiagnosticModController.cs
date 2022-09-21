using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using IkapatigiCapstone.Models;
using IkapatigiCapstone.Data;

namespace IkapatigiCapstone.Controllers
{
    public class DiagnosticModController : Controller
    {
        private readonly ApplicationDbContext context;
        //ApplicationDbContext appCon = new ApplicationDbContext();
        public DiagnosticModController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult AddRequestDiagnostic()
        {
            //ViewBag.TagList = new SelectList();
            return View();
        }
        [HttpPost]
        public IActionResult SendRequest(AddRequestDiagnostic model)
        {
            AddRequestDiagnostic diagReq = new AddRequestDiagnostic();
            diagReq.DateAdded = DateTime.Now;
            diagReq.ImageName = model.ImageName;
            diagReq.DiseaseName = model.DiseaseName;
            diagReq.Srp = model.Srp;
            diagReq.Remarks = model.Remarks;

            context.AddRequestDiagnostics.Add(diagReq);
            return RedirectToAction("Index");
        }
    }
    
    
}
