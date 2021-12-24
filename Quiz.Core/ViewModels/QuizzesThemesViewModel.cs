using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Core.Models;

namespace Quiz.Core.ViewModels
{
    public class QuizzesThemesViewModel
    {
        public List<Quizz> Quizzes { get; set; }
        public List<Theme> Themes { get; set; }
    }
}
