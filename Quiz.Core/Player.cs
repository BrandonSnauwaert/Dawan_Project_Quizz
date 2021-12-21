using Quiz.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core
{
    public class Utilisateur : BaseEntity
    {
        [Required]
        [MaxLength(20), MinLength(3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string ConfirmPWD { get; set; }

        //public TypeUtilisateur TypeUtilisateur { get; set; }

        public string Photos { get; set; }

        public Utilisateur()
        {
        }

        public Utilisateur(string username, string password, string confirmPWD)
        {
            Username = username;
            Password = password;
            ConfirmPWD = confirmPWD;
        }
    }
}
