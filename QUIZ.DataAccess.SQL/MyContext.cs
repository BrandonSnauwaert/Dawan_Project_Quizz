using Quiz.Core;
using Quiz.Core.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace QUIZ.DataAccess.SQL
{
    public class MyContext : DbContext
    {
      
        public MyContext()
            : base("name=MyContext")
        {
        }

        public virtual DbSet<User> Users{ get; set; }
       
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
      
        public virtual DbSet<Quizz> Quizzes { get; set; }
    
        public virtual DbSet<Answer> Answers { get; set; }
 

    }

    
}