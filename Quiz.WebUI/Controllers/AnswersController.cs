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
    public class AnswersController : Controller
    {
        private IServiceRepository<Answer> repo_Answer;
        private IServiceRepository<Question> repo_qst;

        public AnswersController()
        {
            repo_Answer = new ServiceRepository<Answer>(new SQLRepository<Answer>(new MyContext()));
            repo_qst = new ServiceRepository<Question>(new SQLRepository<Question>(new MyContext()));
        }




        // GET: Answers
        public ActionResult Index()
        {
            //var answers = db.Answers.Include(a => a.Question);
            return View(repo_Answer.Collection().ToList());
        }

        // GET: Answers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = repo_Answer.FindById((int)id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(repo_qst.Collection(), "Id", "QstText");
            return View();
        }

        // POST: Answers/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RepText,IsCorrect,QuestionId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                repo_Answer.Insert(answer);
                repo_Answer.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(repo_qst.Collection(), "Id", "QstText", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = repo_Answer.FindById((int)id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(repo_qst.Collection(), "Id", "QstText", answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RepText,IsCorrect,QuestionId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                repo_Answer.Update(answer);
                repo_Answer.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(repo_qst.Collection(), "Id", "QstText", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = repo_Answer.FindById((int)id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo_Answer.DeleteById(id);
            repo_Answer.SaveChanges();
            return RedirectToAction("Index");
        }

     
    }
}
