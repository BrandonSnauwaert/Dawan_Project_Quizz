using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Models
{

    public class Quizz : BaseEntity
    {

        public string Title { get; set; }
        public string ThemeName { get; set; }
        public virtual List<Question> Questions { get; set; }
        public string Image { get; set; }

        public Quizz()
        {
            Questions = new List<Question>();
        }
    }
}
