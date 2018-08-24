using FinalProject_LMS.Models;
using FinalProject_LMS.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalProject_LMS.Controllers
{
    public class UMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UMessages
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            List<UMessageView> messagesList = new List<UMessageView>();
            var messages = db.UMessages.ToList();

            messages = db.UMessages.Where(m => m.ReciverId == UserId).ToList();
            foreach (var m in messages)
            {
                var USender = db.Users.Single(u => u.Id == m.SenderId);
                messagesList.Add(new UMessageView()
                {
                    Id = m.Id,
                    Rubrik = m.Rubrik,
                    Text = m.Text,
                    IsReaden = m.IsReaden,
                    SenderName = USender.Name,
                    ReciverName = user.Name,


                }
                    );

            }






            return View(messagesList);
        }


        // GET: UMessages/OutIndex
        public ActionResult OutIndex()
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            List<UMessageView> messagesList = new List<UMessageView>();
            var messages = db.UMessages.ToList();

            messages = db.UMessages.Where(m => m.SenderId == UserId).ToList();
            foreach (var m in messages)
            {
                var USender = db.Users.Single(u => u.Id == m.SenderId);
                var UReciver = db.Users.Single(u => u.Id == m.ReciverId);
                messagesList.Add(new UMessageView()
                {
                    Id = m.Id,
                    Rubrik = m.Rubrik,
                    Text = m.Text,
                    IsReaden = m.IsReaden,
                    SenderName = USender.Name,
                    ReciverName = UReciver.Name


                }
                    );

            }






            return View(messagesList);
        }




        // GET: UMessages/Create
        public ActionResult Create(string ReciverId = "")
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            var data = db.Users.ToList();



            if (ReciverId == "")
            {
                if (User.IsInRole("Student"))
                {
                    data = db.Users.Where(u => u.CourseId == user.CourseId || u.CourseId == null).ToList();
                }
                else
                {
                    data = db.Users.ToList();
                }

            }
            else
            {
                data = db.Users.Where(u => u.Id == ReciverId).ToList();
            }

            ViewBag.ReciverId = new SelectList(data, "Id", "Name");


            return View();
        }

        // POST: UMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SenderId,Rubrik,Text,ReciverId,IsReaden")] UMessage message)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            message.SenderId = UserId;
            message.IsReaden = 0;
            if (ModelState.IsValid)
            {
                db.UMessages.Add(message);
                db.SaveChanges();


            }
            return RedirectToAction("OutIndex", "UMessages");

        }


        // GET: UMessage/Edit/5
        public ActionResult Edit(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UMessage message = db.UMessages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);
        }

        // POST: UMessage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SenderId,Rubrik,Text,ReciverId,IsReaden")] UMessage message)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            message.IsReaden = 1;
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();


            }

            return RedirectToAction("Index", "UMessages");
        }

        // GET: UMessages/Details
        public ActionResult Details(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            UMessage message = db.UMessages.Single(m => m.Id == id);
            var UReciver = db.Users.Single(u => u.Id == message.ReciverId);
            UMessageView model = new UMessageView()
            {
                Id = message.Id,
                SenderName = user.Name,
                ReciverName = UReciver.Name,
                Rubrik = message.Rubrik,
                Text = message.Text,
                IsReaden = message.IsReaden

            };
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        public JsonResult IsThereAnyNotReadenMessages(string DataName, string text)
        {
            var UserId = User.Identity.GetUserId();
            var user = db.Users.Single(u => u.Id == UserId);
            ViewBag.UserName = user.Name;
            var data = db.UMessages.Where(m => m.ReciverId == user.Id && m.IsReaden == 0).ToList();
            if (data != null)
                return Json(data);

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }
    }
}