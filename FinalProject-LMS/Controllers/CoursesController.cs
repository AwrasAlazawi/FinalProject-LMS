using FinalProject_LMS.Models;
using FinalProject_LMS.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalProject_LMS.Migrations
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        //

        // GET: Courses
        [Authorize]
        public ActionResult AllCourses()
        {
            //GroupBy(x => x.Programa == null ? -1 : x.Programa.Id)
            // var Users = db.Users.Include("Course").Where(u => u.CourseId != null).GroupBy(u => u.CourseId);
            //var x = db.Users.Include("Course").GroupBy(u => u.Course== null? -1:u.CourseId)
            // .Select(y => new AllCoursesViewModels
            // {
            //     Id = y.FirstOrDefault().Course.Id,
            //     Name = y.FirstOrDefault().Course.Name,
            //     Description = y.FirstOrDefault().Course.Description,
            //     StartDate = y.FirstOrDefault().Course.StartDate,
            //     count = y.Count(),
            // }).ToList();

            //if (Users.Count() == 0)
            //{
            List<AllCoursesViewModels> coursesList = new List<AllCoursesViewModels>();
            foreach (var c in db.Courses.ToList())
            {
                coursesList.Add(new AllCoursesViewModels()
                {
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                   // count = db.Users.ToList().Count
                });

            }
            return View(coursesList);
           
        }


   

    [Authorize]
        //GET: Courses/Details/5
        public ActionResult CourseModule(int? id)
        {

            var module = db.Modules.Where(g => g.CourseId == id);
            return View(module);
        }


        [Authorize(Roles ="Teacher")]
        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("AllCourses");
            }

            return View(course);
        }

        // GET: Courses/Edit/5

        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
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

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllCourses");
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
