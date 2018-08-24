using FinalProject_LMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
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
                var user = db.Users.Single(u => u.Id == id);
                ViewBag.UserName = user.Name;

                if (k==2)
                    userManager.AddToRole(id, "Student");
                else if (k==1)
                    userManager.AddToRole(id, "Teacher");
                else
                    return View();

            }

            //LoginViewModel model = new LoginViewModel()
            //{
            //    Email = "alex.gmail.com",
            //    Password = "P@ssw0rd",
            //    RememberMe = true
            //};
            //return RedirectToAction("Login", "Account", new {model });

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