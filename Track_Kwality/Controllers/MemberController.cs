﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Track_Kwality.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult memberForm()
        {
            return View();
        }
        public ActionResult MemberLogin()
        {
            return View();
        }
      
    }
}