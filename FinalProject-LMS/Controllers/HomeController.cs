using FinalProject_LMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject_LMS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? k)
        {

            var id = User.Identity.GetUserId();

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new ApplicationUserManager(userStore);
            if (id != null)
            {

                var userId = User.Identity.GetUserId();
                if (k == 2)
                    userManager.AddToRole(id, "Student");
                else if (k == 1)
                    userManager.AddToRole(id, "Teacher");
                else
                    return View();

                //if (User.IsInRole("Teacher"))
                //     return View();

                //if (User.IsInRole("Student"))
                // return View("Create", "Courses");
            }



            return View();


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}