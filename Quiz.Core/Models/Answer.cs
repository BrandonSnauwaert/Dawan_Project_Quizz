using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Models
{
    public class Answer : BaseEntity
    {

        public string RepText { get; set; }
        public bool IsCorrect { get; set; }

        [ForeignKey("QuestionId")]

        public virtual Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
