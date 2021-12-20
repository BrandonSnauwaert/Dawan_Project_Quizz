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
    public class AdminController : Controller
    {
        private IQuizRepository<User> playerDao;
        private IPlayerService playerService;
        private IPlayerRepository playerCustomRepository;

        public AdminController()
        {
            playerDao = new SQLRepository<User>(new MyContext());
            playerService = new PlayerService(playerDao, playerCustomRepository);
        }
        // GET: Admin
        

        // GET: Admin
        public ActionResult Index()
        {
            List<User> players = playerService.FindPlayers();
            return View(players);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                User player = playerService.FindById((int)id);
                if (player == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(player);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User player, int id)
        {
            if (ModelState.IsValid)
            {
                User uBDD = playerService.FindById(id);
                if (uBDD == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    uBDD.TypeUtilisateur = player.TypeUtilisateur;
                    playerService.Update(uBDD);
                    playerService.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(player);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                User player = playerService.FindById((int)id);
                if (player == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(player);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(User player, int id)
        {
            playerService.DeleteById(id);
            playerService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
    