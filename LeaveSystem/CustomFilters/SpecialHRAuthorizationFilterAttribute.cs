﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveSystem.CustomFilters
{
    public class SpecialHRAuthorizationFilterAttribute:FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if ((filterContext.RequestContext.HttpContext.Session["CurrentUserRoleName"].ToString()!="HR Manager"||filterContext.RequestContext.HttpContext.Session["CurrentUserIsAdmin"].Equals(false))&& filterContext.RequestContext.HttpContext.Session["CurrentUserRoleName"].ToString() != "Project Manager")
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "InvalidAccess" }));
            }
        }
    }
}
