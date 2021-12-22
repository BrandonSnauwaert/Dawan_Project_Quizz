using Quiz.Core.Models;
using QUIZ.DataAccess.SQL.LogicMetier;

namespace Quiz.WebUI.Service
{
    public interface IServiceRepository<T> :IRepository<T> where T :BaseEntity
    {
    }
}