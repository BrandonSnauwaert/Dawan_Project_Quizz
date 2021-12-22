using Quiz.Core.Models;
using System.Linq;

namespace QUIZ.DataAccess.SQL.LogicMetier
{
    /// <summary>
    /// interface pour l'implémentation des fonctionctionalites classiques
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>  where T: BaseEntity
    {

       
        /**
         * return la liste  d'une entité
         */
        IQueryable<T> Collection();
        /**
         * effacer supprimer un élément par son Id
         */
        void DeleteById(int id);
        /**
         * chercher un élément par son Id
         */
        T FindById(int id);
        /**
         * Enregistrement de la rêquete dans la BD
         * 
         */
        void SaveChanges();
        /**
         * MàJ d'un élement dans la BD
         */
        void Update(T t);
        /**
         * insert un élements dans la base de donnée
         */
        void Insert(T t);
    }
}
