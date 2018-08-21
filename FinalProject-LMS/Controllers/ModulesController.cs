using FinalProject_LMS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalProject_LMS.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Modules
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            var course = db.Courses.Single(c => c.Id == user.CourseId);
            ViewBag.CourseName = course.Name;




            if (User.IsInRole("Student"))
            {
                var modules = db.Modules.Where(m => m.CourseId == user.CourseId);
                return View(modules.ToList());
            }

            return View(db.Modules.ToList());
        }



        public ActionResult ModuleActivity(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var activities = db.Activities.Include(a => a.Module).Include(a => a.Type);
            IEnumerable<Activity> activities = db.Activities.Include(a => a.Module).Include(a => a.Type).Where(a => a.ModuleId == id).ToList();
            var Module = db.Modules.Single(m => m.Id == id);
             
            ViewBag.ModuleName = Module.Name;
            
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);

        }

        // GET: Modules/Details/5


        // GET: Modules/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            // ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");

            var data = db.Courses.Where(c => c.Id == id).ToList();
            Course d = db.Courses.Single(c => c.Id == id);
            ViewBag.CourseId = new SelectList(data, "Id", "Name");

            Module model = new Module
            {
                StartDate = d.StartDate,
                EndDate = DateTime.Now

            };
            return View(model);
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate,CourseId")] Module module)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (ModelState.IsValid)
            {
                db.Modules.Add(module);
                db.SaveChanges();

                return RedirectToAction("CourseModule","Courses", new { id = module.CourseId });
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", module.CourseId);
            return View(module);
        }

        // GET: Modules/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", module.CourseId);
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate,CourseId")] Module module)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("CourseModule", "Courses", new { id = module.CourseId });
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", module.CourseId);
            return View(module);
        }

        // GET: Modules/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);

            ViewBag.UserName = user.Name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            var course = db.Courses.Single(c => c.Id == module.CourseId);
            module.Course.Name = course.Name;
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
            db.SaveChanges();
            return RedirectToAction("CourseModule", "Courses", new { id = module.CourseId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
