using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Escuela.MVC.Controllers
{
    public class InstitucionesController : Controller
    {
        // GET: InstitucionesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: InstitucionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InstitucionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstitucionesController/Create
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

        // GET: InstitucionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InstitucionesController/Edit/5
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

        // GET: InstitucionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InstitucionesController/Delete/5
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
