using Quiz.Core.Models;
using QUIZ.DataAccess.SQL.LogicMetier;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUIZ.DataAccess.SQL
{
    public class SQLRepository<T> : IQuizRepository<T> where T : BaseEntity
    {
      
            //Regroupe l'ensemble des entité gérées
            internal MyContext DataContext;

            internal DbSet<T> dbSet;

            public SQLRepository(MyContext dataContext)
            {
                DataContext = dataContext;

                //Crée un DbSet pour l'entité T : On va recuperer le dbSet de l'entité T grâce au contexte
                dbSet = DataContext.Set<T>();
            }


            public IQueryable<T> Collection()
            {
                return dbSet;
            }


            public T FindById(int id)
            {
         
                return dbSet.AsNoTracking().SingleOrDefault(x => x.Id == id);
            }

            public void DeleteById(int id)
            {
                T t = FindById(id);

                if (DataContext.Entry(t).State == EntityState.Detached)
                {
                    //Attachee l'entité donnée au contexte
                    dbSet.Attach(t);
                }

                dbSet.Remove(t);
            }

            public void Insert(T t)
            {
                dbSet.Add(t);
            }

            public void SaveChanges()
            {
                DataContext.SaveChanges();
            }

            public void Update(T t)
            {
                dbSet.Attach(t); //Charge l'objet t dans le context
                DataContext.Entry(t).State = EntityState.Modified;
            }
        
    }
}
