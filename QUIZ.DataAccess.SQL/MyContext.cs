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

        public DbSet<User> Players { get; set; }
       
        public DbSet<Question> Questions { get; set; }
        public DbSet<Theme> Themes { get; set; }
      
        public virtual DbSet<Quiz.Core.Models.Quizz> Quizzes { get; set; }
    
        public virtual DbSet<Answer> Answers { get; set; }
 

    }

    
}