using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///  Accés restrainte aux players (cest l'admin qui a la possibilité de rentrée les questions et les choix)
///  
/// </summary>
namespace Quiz.Core.Models
{
    public class Question : BaseEntity
    {
        public string QstText { get; set; }
        public bool IsMultiple { get; set; }
        public int NumOrder { get; set; }

        [ForeignKey("QuizId")]
        public Quizz Quiz { get; set; }
        public int QuizId { get; set; }

        public virtual List<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }

    }
}
