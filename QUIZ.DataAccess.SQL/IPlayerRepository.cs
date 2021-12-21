using Quiz.Core;
using Quiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUIZ.DataAccess.SQL
{
    public interface IPlayerRepository
    {
            User FindByUsername(string username);
    
    }
}
