using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project1.Models;
using Project1.ViewModels;

public class UserController : Controller
{
    Project1Entities db = new Project1Entities();

    public ActionResult Index()
    {
        return View();
    }
    // GET: User/Register
    public ActionResult Register()
    {
        if (Session["UserId"] != null || Session["AdminId"] != null)
        {
            return RedirectToAction("Logout");
        }
        return View();
    }

    // POST: User/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                MobileNumber = model.MobileNumber,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                ProfileImage = SaveProfileImage(model.ProfileImage),
                isActive = true
            };

            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        return View(model);
    }

    // Save profile image and return path
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

    // GET: User/Login
    public ActionResult Login()
    {
        if(Session["UserId"] != null || Session["AdminId"] != null)
        {
            return RedirectToAction("Logout");
        }
        return View();
    }

    // POST: User/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = db.Users.SingleOrDefault(u => (u.Email == model.EmailOrMobile || u.MobileNumber == model.EmailOrMobile) && u.Password == model.Password);
            var admin = db.Admins.SingleOrDefault(a => (a.Email == model.EmailOrMobile) && a.Password == model.Password);
            if(admin != null)
            {
                FormsAuthentication.SetAuthCookie(admin.Email, false);
                Session["AdminId"] = admin.AdminId;
                return RedirectToAction("Index", "Admin");
            }
            if (user != null)
                {
                    if (!user.isActive)
                    {
                        ViewBag.Message = "Your account has been deactivated";
                        ModelState.Clear();
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, false);
                        Session["UserId"] = user.UserId;
                        return RedirectToAction("Index", "Home");
                    }
                }
            else
            {
                ViewBag.Message = "Incorrect Credentials, Please Try Again.";
                ModelState.Clear();
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }
        return View(model);
    }

    public ActionResult Logout()
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        return RedirectToAction("Login");
    }

}
