using Quiz.Core.Models;
using Quiz.Core.ViewModels;
using Quiz.WebUI.Service;
using QUIZ.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiz.WebUI.Controllers
{
    public class HomeController : Controller
    {
        

        private IServiceRepository<Quizz> repo_quiz;
        private IServiceRepository<Theme> repo_theme;
        private IServiceRepository<Question> repo_qst;
        private IServiceRepository<Answer> repo_answer;

        public HomeController()
        {
            repo_quiz = new ServiceRepository<Quizz>(new SQLRepository<Quizz>(new MyContext()));
            repo_theme = new ServiceRepository<Theme>(new SQLRepository<Theme>(new MyContext()));
            repo_qst = new ServiceRepository<Question>(new SQLRepository<Question>(new MyContext()));
            repo_answer = new ServiceRepository<Answer>(new SQLRepository<Answer>(new MyContext()));
        }

        public ActionResult Index(string Theme = null)
        {
            List<Quizz> Quizs;
            List<Theme> Themes= repo_theme.Collection().ToList();

            if (Theme== null)
            {
                Quizs = repo_quiz.Collection().ToList();
            }
            else
            {
                Quizs = repo_quiz.Collection().Where(q => q.ThemeName== Theme).ToList();
            }

            ListQuizThemeVM model = new ListQuizThemeVM();
            model.GetQuizzs= Quizs;
            model.GetThemes = Themes;

            return View(model);
        }

        public ActionResult Demarrer(int id)
        {
            Session["score"] = 0;
            Session["quizId"] = id;
            Session["order"] = 1;

            Question qst =repo_qst.Collection().SingleOrDefault(q => q.QuizId == id && q.NumOrder == 1);


            return View("Progression", qst);
        }

        [HttpPost]
        public ActionResult Next(FormCollection form)
        {
            int score = (int)Session["score"];
            int selectedQuizId = (int)Session["quizId"];
            int order = (int)Session["order"];

            Quizz qcm = repo_quiz.Collection().SingleOrDefault(q => q.Id == selectedQuizId);
            Question qstEnCours = qcm.Questions[order - 1];

            if (!qstEnCours.IsMultiple)
            {
                int idSelectedRep = Convert.ToInt32(form.Get("selectedSimpleRep"));

                if (repo_answer.Collection().SingleOrDefault(r => r.Id == idSelectedRep).IsCorrect)
                {
                    score++;
                    Session["score"] = score;
                }
                else
                {
                    score--;
                    Session["score"] = score;
                }
            }
            else
            {
                string[] reponses = form.GetValues("selectedRep[]");
                bool[] tabRep = new bool[reponses.Length];
                for (int i = 0; i < reponses.Length; i++)
                {
                    int idRepEnCours = Convert.ToInt32(reponses[i]);
                    tabRep[i] = repo_answer.Collection().SingleOrDefault(r => r.Id == idRepEnCours).IsCorrect;
                }
                bool exist = tabRep.Contains(false);
                if (exist)
                {
                    score--;
                    Session["score"] = score;
                }
                else
                {
                    score++;
                    Session["score"] = score;
                }

            }

            //Passer à la question suivante
            if (order < qcm.Questions.Count)
            {
                order++;
                Session["order"] = order;
                Question qst = repo_qst.Collection().SingleOrDefault(q => q.QuizId == selectedQuizId && q.NumOrder == order);
                return View("Progression", qst);
            }
            else
            {
                
                //Envoyer le résultat par email
                return View("Result");
            }


        }
    }
}