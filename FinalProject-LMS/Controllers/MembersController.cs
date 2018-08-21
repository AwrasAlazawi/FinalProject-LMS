using FinalProject_LMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace FinalProject_LMS.Controllers
{
    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: Members
        public ActionResult Index()
        {
            return View();
        }


        ////Get
        //[AllowAnonymous]
        //public ActionResult Register(int? k)
        //{
        //    ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
        //    RegisterViewModel model = new RegisterViewModel
        //    {
        //        Kind = k
        //    };
        //    return View(model);

        //}


        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(string Email, string Name, string Password, string ConfirmPassword, int? CourseId, int? Kind)
        //{
        //    var userStore = new UserStore<ApplicationUser>(db);
        //    var userManager = new ApplicationUserManager(userStore);

        //    var user = new ApplicationUser
        //    {
        //        Name = Name,
        //        UserName = Email,
        //        Email = Email,
        //        CourseId = CourseId,


        //    };
        //    //db.Users.Add(user);
        //    //db.SaveChanges();
        //    var result = userManager.Create(user, Password);
        //    var User = userManager.FindByName(Email);
        //    if (Kind == 1)
        //    {
        //        userManager.AddToRole(User.Id, "Teacher");
        //    }
        //    else
        //    {
        //        userManager.AddToRole(User.Id, "Student");
        //    }

        //    return RedirectToAction("Index", "Home");
        //}


        //Get
        [AllowAnonymous]
        public ActionResult RegisterMember()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");

            return View();

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterMember(string Email, string Name, string Password, string ConfirmPassword, int? CourseId, int? Kind)
        {
          


            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new ApplicationUserManager(userStore);
            var user = new ApplicationUser();
            if (Kind == 1)
            {
                user = new ApplicationUser
                {
                    Name = Name,
                    UserName = Email,
                    Email = Email,
                    CourseId = null


                };

            }
            else
            {
                user = new ApplicationUser
                {
                    Name = Name,
                    UserName = Email,
                    Email = Email,
                    CourseId = CourseId


                };
            }

            //db.Users.Add(user);
            //db.SaveChanges();
            var result = userManager.Create(user, Password);
            var User = userManager.FindByName(Email);
            if (Kind == 1)
            {
                userManager.AddToRole(User.Id, "Teacher");
                return RedirectToAction("AllTeachers", "Account");
            }
            else
            {
                userManager.AddToRole(User.Id, "Student");
                return RedirectToAction("AllStudents", "Account");
            }

            
        }

    }
}