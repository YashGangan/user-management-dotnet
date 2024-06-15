using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1.Models;

namespace Project1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Project1Entities db = new Project1Entities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                var users = db.Users.ToList();
                return View(users);
            }

            return RedirectToAction("Login", "User");
        }

        // GET: Admin/ActivateDeactivate
        public ActionResult Deactivate(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                user.isActive = !user.isActive;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}