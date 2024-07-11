using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project1.Models;
using PagedList;
using PagedList.Mvc;

namespace Project1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Project1Entities db = new Project1Entities();
        // GET: Admin
        public ActionResult Index(int? page)
        {
            if (Session["AdminId"] != null)
            {
                int pageSize = 5; // number of users per page
                int pageNumber = (page ?? 1); // when no page is specified 

                var users = db.Users.OrderBy(u => u.UserId).ToPagedList(pageNumber, pageSize);
                
                // var users = db.Users.ToList();
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