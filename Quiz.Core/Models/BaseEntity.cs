using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Models
{
   public  class BaseEntity
    {
        /// <summary>
        /// l'attribut Id pour toutes les classes qui hérite la classe BaseEntity
        /// </summary>
        public int Id { get; set; }
    }
}
