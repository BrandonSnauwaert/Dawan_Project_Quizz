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
        private IQuizRepository<User> playerDao;
        private IPlayerService playerService;
        private IPlayerRepository playerCustomRepository;

        public UserController()
        {
            playerDao = new SQLRepository<User>(new MyContext());
            playerCustomRepository = new PlayerRepository(new MyContext());
            playerService = new PlayerService(playerDao, playerCustomRepository);
        }


        // GET: Player
        public ActionResult Index()
        {
            List<User> players = playerService.FindPlayers();
            return View(players);
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
    }
}