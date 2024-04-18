using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;
using DAL;

namespace Track_Kwality.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var loginDAL = new LoginBLL();
            var loginResult = await loginDAL.AuthenticateUser(model);
            if (loginResult == "1" || loginResult == "2" || loginResult == "3" || loginResult == "4")
            {
                ViewBag.LoginResult = loginResult;
                HttpCookie userCookie = new HttpCookie("UserDetails");
                userCookie["UserId"] = model.UserId;
                userCookie["LoginResult"] = loginResult;
                userCookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(userCookie);
                FormsAuthentication.SetAuthCookie(model.UserId, false);
                return RedirectToAction("dashboard", "Dashboard");
            }
            ViewBag.Message = "Wrong UserId or Password";
            return View();
        }

       

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}