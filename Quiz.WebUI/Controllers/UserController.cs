using Quiz.Core;
using Quiz.Core.Models;
using Quiz.WebUI.Service;
using QUIZ.DataAccess.SQL;
using QUIZ.DataAccess.SQL.LogicMetier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Quiz.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IServiceRepository<User> repo;
        
        public UserController()
        {
            repo = new ServiceRepository<User>(new SQLRepository<User>(new MyContext()));
        }


        // GET: Player
        public ActionResult Index()
        {
            
            return View(repo.Collection().ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User player = repo.FindById((int) id);
            if (player == null)
            {
                return HttpNotFound();
            }

            return View(player);
        }
    }
}