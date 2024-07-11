﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project1.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Mobile Number must be 10 digits long")]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }
    }

}