using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project1.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }

        public string ProfileImagePath { get; set; }
    }

}