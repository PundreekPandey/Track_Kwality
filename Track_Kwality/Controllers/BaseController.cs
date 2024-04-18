using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Track_Kwality.Helper;

namespace Track_Kwality.Controllers
{
    public class BaseController : Controller
    {
        protected string UserId
        {
            get
            {
                if (Request.Cookies["UserDetails"] != null)
                {
                    return Request.Cookies["UserDetails"]["UserId"];
                }
                return null; // Cookie or UserId not found
            }
        }

        protected string LoginResult
        {
            get
            {
                if (Request.Cookies["UserDetails"] != null)
                {
                    return Request.Cookies["UserDetails"]["LoginResult"];
                }
                return null; // Cookie or LoginResult not found
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.UserID = UserId;
            ViewBag.LoginResult = LoginResult;
            base.OnActionExecuting(filterContext);
        }
    }
}