﻿using KontaktHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KontaktHome.Filters
{
    public class AuthSeller : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User != null && CurrentSession.User.IsSeller == false)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}