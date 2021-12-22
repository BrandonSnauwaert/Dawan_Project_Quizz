using System.Linq;
using System.Net;
using System.Web.Mvc;
using Quiz.Core.Models;
using Quiz.WebUI.Service;
using QUIZ.DataAccess.SQL;

namespace Quiz.WebUI.Controllers
{
    public class QuestionsController : Controller
    {
        
        private IServiceRepository<Question> repo_qst;
        private IServiceRepository<Quizz> repo_quiz;

        public QuestionsController()
        {
            repo_qst = new ServiceRepository<Question>(new SQLRepository<Question>(new MyContext()));
            repo_quiz = new ServiceRepository<Quizz>(new SQLRepository<Quizz>(new MyContext()));
        }



        // GET: Questions
        public ActionResult Index()
        {
            
            return View(repo_qst.Collection().ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = repo_qst.FindById((int)id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.QuizId = new SelectList(repo_quiz.Collection(), "Id", "Title");
            Question model = new Question();
            return View(model);
        }

        // POST: Questions/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QstText,IsMultiple,NumOrder,QuizId")] Question question)
        {
            if (ModelState.IsValid)
            {
                repo_qst.Insert(question);
                repo_qst.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuizId = new SelectList(repo_quiz.Collection(), "Id", "Title", question.QuizId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = repo_qst.FindById((int)id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizId = new SelectList(repo_quiz.Collection(), "Id", "Title", question.QuizId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QstText,IsMultiple,NumOrder,QuizId")] Question question)
        {
            if (ModelState.IsValid)
            {
                repo_qst.Update(question);
                repo_qst.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuizId = new SelectList(repo_quiz.Collection(), "Id", "Title", question.QuizId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = repo_qst.FindById((int)id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo_qst.DeleteById(id);
            repo_qst.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
