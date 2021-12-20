using Quiz.Core;
using Quiz.Core.Models;
using Quiz.Core.Tools;
using QUIZ.DataAccess.SQL;
using QUIZ.DataAccess.SQL.LogicMetier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.WebUI.Service
{
    public class PlayerService : IPlayerService
    {
        private IQuizRepository<User> userDao;
        private IPlayerRepository playerCustomRepository;

        public PlayerService()
        {

        }

        public PlayerService(IQuizRepository<User> userDao, IPlayerRepository playerCustomRepository)
        {
            this.userDao = userDao;
            this.playerCustomRepository = playerCustomRepository;
        }
        public User CheckLogin(string username, string password)
        {
            

        User p = playerCustomRepository.FindByUsername(username);
            string pwdCrypt = password;

            pwdCrypt = HashTools.ComputeSha256Hash(password);

            if (p == null || !p.Password.Equals(pwdCrypt))
            {
                return null;
            }
            return p;
        }

        public void DeleteById(int id)
        {
            userDao.DeleteById(id);
        }

        public User FindById(int id)
        {
            return userDao.FindById(id);
        }

        public User FindByUsername(string username)
        {
            return playerCustomRepository.FindByUsername(username);
        }
        public List<User> FindPlayers()
        {
            return userDao.Collection().ToList();
        }

        public void InsertUser(User p)
        {
            //On va verifier que cet utilisateur n'existe pas dans la base de données
            VerifyUsername(p);

            //Avant d'inserer l'utilisateur 
            //On met à our le password de l'utilisateur avec le password crypté
            p.Password = HashTools.ComputeSha256Hash(p.Password);
            p.ConfirmPWD = HashTools.ComputeSha256Hash(p.Password);

            userDao.Insert(p);
        }

        private void VerifyUsername(User p)
        {
            List<User> userFromBDD = FindPlayers();
            for (int i = 0; i < userFromBDD.Count; i++)
            {
                if (p.Username == userFromBDD[i].Username)
                {
                    throw new Exception("Le UserName existe déjà. Veuillez choisir un autre Username!!");
                }

              
            }
        }

        public void SaveChanges()
        {
            userDao.SaveChanges();
        }

        public void Update(User p)
        {
            User userBDD = userDao.FindById(p.Id);
            if (!userBDD.Password.Equals(p.Password))
            {
                p.Password = HashTools.ComputeSha256Hash(p.Password);
                p.ConfirmPWD = HashTools.ComputeSha256Hash(p.ConfirmPWD);
            }

            userDao.Update(p);
        }
    }
}