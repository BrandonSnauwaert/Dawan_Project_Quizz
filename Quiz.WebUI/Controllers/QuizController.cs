using Quiz.Core.Models;
using Quiz.Core.ViewModels;
using QUIZ.DataAccess.SQL;
using QUIZ.DataAccess.SQL.LogicMetier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Quiz.WebUI.Controllers
{
    public class QuizController : Controller
    {

        private IRepository<Quizz> quizDao;
        private IRepository<Theme> themeDao;

        public QuizController()
        {
            quizDao = new SQLRepository<Quizz>(new MyContext());
            themeDao = new SQLRepository<Theme>(new MyContext());
        }

        // GET: Quiz
        public ActionResult Index()
        {
            return View(quizDao.Collection().ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Quizz quiz = quizDao.FindById((int)id);
            if (quiz == null)
                return HttpNotFound();

            return View(quiz);
        }


        public ActionResult Create()
        {
            QuizThemesViewModel model = new QuizThemesViewModel();
            model.Quiz = new Quizz();
            model.Themes = themeDao.Collection().ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] Quizz quiz, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                int maxId = 0;
                if (quizDao.Collection().Count() != 0)
                    maxId = quizDao.Collection().Max(q => q.Id);

                quiz.Image = (maxId + 1) + Path.GetExtension(image.FileName);
                image.SaveAs(Server.MapPath("~/Content/Images/Quiz/") + quiz.Image);
                quizDao.Insert(quiz);
                quizDao.SaveChanges();

                return RedirectToAction("Index");
            }

            QuizThemesViewModel model = new QuizThemesViewModel();
            model.Quiz = quiz;
            model.Themes = themeDao.Collection().ToList();

            return View(model);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Quizz quiz = quizDao.FindById((int)id);
            if (quiz == null)
                return HttpNotFound();

            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] Quizz quiz, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    string imagePath = Server.MapPath("~/Content/Images/Quiz/") + quiz.Id + Path.GetExtension(image.FileName);
                    if (System.IO.File.Exists(imagePath))
                        System.IO.File.Delete(imagePath);

                    image.SaveAs(imagePath);
                    quiz.Image = quiz.Id + Path.GetExtension(image.FileName);
                }

                quizDao.Update(quiz);
                quizDao.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(quiz);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Quizz quiz = quizDao.FindById((int)id);
            if (quiz == null)
                return HttpNotFound();

            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            quizDao.DeleteById(id);
            quizDao.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}