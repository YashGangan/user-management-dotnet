using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Project1.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string EmailOrMobile { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }

}