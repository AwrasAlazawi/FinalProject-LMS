using FinalProject_LMS.Models;
using FinalProject_LMS.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalProject_LMS.Migrations
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            List<CourseView> coursesList = new List<CourseView>();
            if (User.IsInRole("Student"))
            {
                var cousre = db.Courses.Where(c => c.Id == user.CourseId).ToList();
                foreach (var c in cousre)
                {
                    

                    coursesList.Add(new CourseView()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        StartDate = c.StartDate,
                        NumberOfStudents = 0



                    }
                        );

                }

                

            }
            else
            {
                foreach (var c in db.Courses.ToList())
                {
                    int number = db.Users.Where(u => u.CourseId == c.Id).Count();

                    coursesList.Add(new CourseView()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        StartDate = c.StartDate,
                        NumberOfStudents = number



                    }
                        );

                }

            }

         
          

            return View(coursesList);
        }
        // GET: Courses/Details/5
        public ActionResult Details()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            Course course = db.Courses.Single(c => c.Id == user.CourseId );
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        //GET: Courses/Module/5
        public ActionResult CourseModule(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
         

            var module = db.Modules.Where(g => g.CourseId == id);
           
            var Course = db.Courses.SingleOrDefault(c => c.Id == id);
            if (Course == null)
                return RedirectToAction("Index", "Courses");
            ViewBag.CourseName = Course.Name;
            return View(module);
        }
    

        // GET: Courses/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate")] Course course)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        public JsonResult UniqueCourseName(string DataName, string text)
        {
            if (DataName == "Name")
            {
                var data = db.Courses.Where(c => c.Name.Equals(text.Trim(), StringComparison.InvariantCultureIgnoreCase)).Select(c => new { text = c.Name }).ToList();
                if (data != null)
                    return Json(data);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }

        // GET: Courses/Edit/5
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
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate")] Course course)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
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
