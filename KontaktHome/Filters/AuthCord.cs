using KontaktHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KontaktHome.Filters
{
    public class AuthCord : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User != null && CurrentSession.User.IsCord == false)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}