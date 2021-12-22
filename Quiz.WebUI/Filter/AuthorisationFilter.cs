using QUIZ.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiz.WebUI.Filter
{
    public class AuthorisationFilter : AuthorizeAttribute
    {
      
            private string[] allowedRoles;

            public AuthorisationFilter(params string[] allowedRoles)
            {
                this.allowedRoles = allowedRoles;
            }

            //Traiter les requêtes autorisée

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                //Récupérer le username stocké en session
                int type_user = (int)httpContext.Session["TypeUtilisateur"];
                bool autorize = false;

                if (type_user == 1)
                {
                    //Récupérer le role du user depuis la BD
                    using (var context = new MyContext())
                    {
                        var userRole = (from u in context.Users
                                        join r in context.Users on u.Id equals r.Id
                                        where(int)u.TypeUtilisateur == type_user
                                        select new { r.Username }).FirstOrDefault();

                        //Vérifier si userRole est contenu dans le tableau allowedRoles

                        foreach (var role in allowedRoles)
                        {
                            if (role == userRole.Username)
                            {
                                //User autorisé à executer l'action
                                autorize = true;
                            }
                        }
                    }

                }
                return autorize;
            }


            //Traiter les requêtes non autorisées - si autorize = false --- cette méthode sera executée

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                //Redirection vers /Home/NonAutoriser
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
            }





        
    }
}