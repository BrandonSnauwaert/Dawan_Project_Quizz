using Quiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.ViewModels
{
   public  class ListQuizThemeVM
    {
        
        public List<Quizz> GetQuizzs {get; set; }
        public List<Theme> GetThemes {get; set; }
       
    }

}
