using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAL
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please Enter User ID")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public string loginResult { get; set; }
    }
}