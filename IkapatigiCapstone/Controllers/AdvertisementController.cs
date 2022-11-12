using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IkapatigiCapstone.Controllers
{
    public class AdvertisementController : Controller
    {
        // GET: AdvertisementController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdvertisementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvertisementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvertisementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertisementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvertisementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertisementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdvertisementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
