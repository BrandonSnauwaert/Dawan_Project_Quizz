using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quiz.Core.Models;
using Quiz.WebUI.Service;
using QUIZ.DataAccess.SQL;

namespace Quiz.WebUI.Controllers
{
    public class ThemesController : Controller
    {
        private MyContext db = new MyContext();
        private IServiceRepository<Theme> repo;

        public ThemesController()
        {
            repo = new ServiceRepository<Theme>(new SQLRepository<Theme>(db));
        }


        // GET: Categories
        public ActionResult Index()
        {
            return View(repo.Collection().ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theme theme = repo.FindById((int)id);
            if (theme == null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ThemeName")] Theme theme)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(theme);
                repo.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theme);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theme theme = repo.FindById((int)id);
            if (theme== null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        // POST: Categories/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ThemeName")] Theme theme)
        {
            if (ModelState.IsValid)
            {
                repo.Update(theme);
                repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theme);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theme theme = repo.FindById((int)id);
            if (theme == null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteById(id);
            repo.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
