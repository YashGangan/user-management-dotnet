using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project1.ViewModels;
using Project1.Models;

namespace Project1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Project1Entities db = new Project1Entities();

        // GET: Home/Index
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var user = db.Users.Find(userId);
            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Login", "User");
        }

        // GET: Home/EditProfile
        public ActionResult EditProfile()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var user = db.Users.Find(userId);
            if (user != null)
            {
                var model = new EditProfileViewModel
                {
                    MobileNumber = user.MobileNumber,
                    Username = user.Username,
                    Email = user.Email,
                    ProfileImagePath = user.ProfileImage
                };
                return View(model);
            }

            return RedirectToAction("Login", "User");
        }

        // POST: Home/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["UserId"]);
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    user.MobileNumber = model.MobileNumber;
                    user.Username = model.Username;
                    user.Email = model.Email;
                    if (model.ProfileImage != null)
                    {
                        user.ProfileImage = SaveProfileImage(model.ProfileImage);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        public string SaveProfileImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Images"), fileName);
                file.SaveAs(path);
                return "/Images/" + fileName;
            }
            return "/Images/default-image.png";
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

    }
}