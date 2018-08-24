using FinalProject_LMS.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalProject_LMS.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            var activities = db.Activities.Include(a => a.Module).Include(a => a.Type);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            var data = db.Modules.Where(m => m.Id == id).ToList();
            ViewBag.ModuleId = new SelectList(data, "Id", "Name");

            ViewBag.TypeId = new SelectList(db.ActivityTypes, "Id", "Name");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TypeId,ModuleId,StartTime,EndTime")] Activity activity)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("ModuleActivity","Modules",new { id = activity.ModuleId });
            }

            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", activity.ModuleId);
            ViewBag.TypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.TypeId);
            return View(activity);
        }

        // GET: Activities/Edit/5
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
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", activity.ModuleId);
            ViewBag.TypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.TypeId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TypeId,ModuleId,StartTime,EndTime")] Activity activity)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ModuleActivity", "Modules", new { id = activity.ModuleId  });
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "Name", activity.ModuleId);
            ViewBag.TypeId = new SelectList(db.ActivityTypes, "Id", "Name", activity.TypeId);
            return View(activity);
        }

        // GET: Activities/Delete/5
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
            Activity activity = db.Activities.Find(id);
           
            var module = db.Modules.Single(c => c.Id == activity .ModuleId);
            var type = db.ActivityTypes.Single(a => a.Id == activity.TypeId);
            activity.Module .Name = module .Name;
            activity.Type.Name = type.Name;
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("ModuleActivity", "Modules", new { id = activity.ModuleId });
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
