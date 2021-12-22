using Quiz.Core;
using Quiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUIZ.DataAccess.SQL
{
    public class PlayerRepository : IPlayerRepository
    {
        private MyContext DataContext;

        public PlayerRepository(MyContext dataContext)
        {
            DataContext = dataContext;
        }

        public User FindByUsername(string username)
        {

            return DataContext.Users.SingleOrDefault(p => p.Username == username);
        }
    }
}