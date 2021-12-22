using Quiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.WebUI.Service
{
    public class ServiceRepository<T> : IServiceRepository<T> where T:BaseEntity
    {
        public IQueryable<T> Collection()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T t)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}