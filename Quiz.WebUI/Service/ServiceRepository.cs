using Quiz.Core.Models;
using QUIZ.DataAccess.SQL.LogicMetier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.WebUI.Service
{
    public class ServiceRepository<T> : IServiceRepository<T> where T:BaseEntity
    {
        private IRepository<T> dao;

        public ServiceRepository(IRepository<T> dao)
        {
            this.dao = dao;
        }

       
        public IQueryable<T> Collection()
        {
            return dao.Collection();
        }

        public void DeleteById(int id)
        {
            dao.DeleteById(id);
        }

        public T FindById(int id)
        {
            return dao.FindById(id);
        }

        public T FindByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Insert(T t)
        {
            dao.Insert(t);
        }

        public void SaveChanges()
        {
            dao.SaveChanges();
        }

        public void Update(T t)
        {
            dao.Update(t);
        }
    }
}