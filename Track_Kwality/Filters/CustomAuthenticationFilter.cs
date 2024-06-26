﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Track_Kwality.Filters
{
    public class CustomAuthenticationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }

    }
}