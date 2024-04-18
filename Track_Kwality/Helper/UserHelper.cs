using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Track_Kwality.Helper
{
    public class UserHelper
    {
        public static LoginViewModel GetLoggedInUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return new LoginViewModel { UserId = HttpContext.Current.User.Identity.Name};
            }

            return null;
        }
    }
}