using Quiz.Core;
using Quiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.WebUI.Service
{
    interface IPlayerService
    {
        User CheckLogin(string username, string password);
        void InsertUser(User p);
        void SaveChanges();

        User FindByUsername(string username);
        List<User> FindPlayers();
        User FindById(int id);
        void DeleteById(int id);
        void Update(User p);
    }
}
