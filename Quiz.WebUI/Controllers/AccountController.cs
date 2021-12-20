using Quiz.Core;
using Quiz.Core.Models;
using Quiz.WebUI.Service;
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
    
    public class AccountController : Controller
    {
        private IQuizRepository<User> playerDao;
        private IPlayerService playerService;
        private IPlayerRepository playerCustomRepository;

        public AccountController()
        {
            playerDao = new SQLRepository<User>(new MyContext());
            playerCustomRepository = new PlayerRepository(new MyContext());
            playerService = new PlayerService(playerDao, playerCustomRepository);
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // Retourner créer un compte
        public ActionResult Register()
        {
            return View();
        }
        // Action pour créer un compte
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User p)
        {
            if (ModelState.IsValid)
            {
                if (p.Password.Equals(p.ConfirmPWD))
                {
                    User player = new User(p.Username, p.Password, p.ConfirmPWD);
                    
                    try
                    {
                        playerService.InsertUser(player);  //Ajoute l'entité p au contexte
                    }
                    catch (Exception eUserName)
                    {
                        ViewBag.ExceptionUserName = eUserName.Message;
                        return View();
                    }
                    

                    playerService.SaveChanges();  //Ensuite l'ajoute dans la base de données

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.ErrorLog = "Les deux mots de passe ne correspondant pas";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        //Connexion sur le site
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String username, string password)
        {
            if (ModelState.IsValid)
            {
                User p = playerService.CheckLogin(username, password);
                if (p == null)
                {
                    ModelState.Clear();
                    ViewBag.ErrorLog = "Le nom d'utilisateur renseigné n'existe pas ou le mot de passe est faux";
                    return View();
                }
                else
                {
                    //Connexion a reussi
                    Session["Connexion"] = p.Username;
                    Session["Id"] = p.Id;

                    Session["TypeUtilisateur"] = p.TypeUtilisateur;
                    //Session["Photo"] = p.Photos;

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorLog = "Le pseudo ou le mot de passe ne sont pas renseignés";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            Session["Connexion"] = null;
            Session["Id"] = null;

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User player = playerService.FindById((int)id);

            if (player == null)
            {
                return HttpNotFound();
            }

            return View(player);

        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User player = playerService.FindById((int)id);
            if (player == null)
            {
                return HttpNotFound();
            }

            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                
                playerService.Update(user);
                playerService.SaveChanges();

                return RedirectToAction("Details", new { Id = user.Id });
                
            }


        }
    }
}