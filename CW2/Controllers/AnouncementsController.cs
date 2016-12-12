using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CW2.Models;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace CW2.Controllers
{   
    [Authorize]
    public class AnouncementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IEnumerable<string> HowMany(Anouncement Anouncement)
        {

            StudentRead Read1 = new StudentRead();

            //Finds which user we got
            string CurrentUser = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == CurrentUser);

            Read1.AnnounceId = Anouncement;
            Read1.UserId = user;
            db.StudentRead.Add(Read1);
            db.SaveChanges();

            var Counte = (from db in db.StudentRead
                          where db.AnnounceId.Id == Anouncement.Id
                          select db.UserId.Id).AsEnumerable();

            return Counte;
        }

        private List<Comment> TableContent(int id)
        {
            var SpecifcCom = (from d in db.Comments
                              where d.CompareFig == id
                              select d).ToList();

            return SpecifcCom;
        }

        public ActionResult Index()
        {
            return View(db.Anouncements.ToList());
        }

        public ActionResult BuildTable(int id)
        {
            return PartialView("_CommentSection", TableContent(id));
        }

        // GET: Anouncements/Details/5
        public ActionResult Details(int? id)
        {
            AnouncementDetailView vm = new AnouncementDetailView();
            Anouncement anouncement = db.Anouncements.Find(id);
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (anouncement == null)
            {
                return HttpNotFound();
            }

            var Output = HowMany(anouncement).Distinct().Count();
            anouncement.CountRe = Output;
            vm.announcement = anouncement;
            return View(vm);
        }

        // GET: Anouncements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anouncements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public ActionResult Create([Bind(Include = "Id,Title,Description")] Anouncement anouncement)
        {
            if (ModelState.IsValid)
            {

                string currentUser = User.Identity.GetUserId();
                ApplicationUser user = db.Users.FirstOrDefault(
                    x => x.Id == currentUser);
                anouncement.Lecture = user;

                db.Anouncements.Add(anouncement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anouncement);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anouncement comment = db.Anouncements.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Anouncements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public ActionResult Edit([Bind(Include = "Id,Title,Description")] Anouncement anouncement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anouncement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anouncement);
        }

        // GET: Anouncements/Delete/5
        [Authorize(Roles = "Lecturer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anouncement anouncement = db.Anouncements.Find(id);
            if (anouncement == null)
            {
                return HttpNotFound();
            }
            return View(anouncement);
        }

        // POST: Anouncements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public ActionResult DeleteConfirmed(int id)
        {
            Anouncement anouncement = db.Anouncements.Find(id);
            db.Anouncements.Remove(anouncement);
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
