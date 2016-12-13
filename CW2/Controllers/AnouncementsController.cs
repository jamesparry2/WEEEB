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

        /*
         * Instantiate a StudentRead Object and then finds the currentuser in that Instances, we
         * then assign that User to the Object and then perform a LINQ command to find the Users who
         * have read that page, once done return that list to get Distinct.Count to find the total of
         * people who have viewed that page.
        */
        private IEnumerable<string> HowMany(Anouncement Anouncement)
        {
            StudentRead Read1 = new StudentRead();
            string CurrentUser = User.Identity.GetUserId();
            ApplicationUser Users = db.Users.FirstOrDefault(x => x.Id == CurrentUser);
            var UsersInRole = db.Roles.SingleOrDefault(r => r.Name == "Student").Users;
            var Students = UsersInRole.Count;

            if (User.IsInRole("Student"))
            {
                Read1.AnnounceId = Anouncement;
                Read1.UserId = Users;
                db.StudentRead.Add(Read1);
                db.SaveChanges();
            }

            var Counte = (from db in db.StudentRead
                          where db.AnnounceId.Id == Anouncement.Id
                          select db.UserId.Id).AsEnumerable();

            ViewBag.Seen = Math.Round(100f * ((float)Counte.Distinct().Count() / (float)Students));

            /*
            var AllUsers = (from db in db.Users
                            select db.Id).AsEnumerable();

            var NotRead = AllUsers.Except(Counte);
            */
            return Counte;
        }

        public ActionResult UsersWhoHaveNotViewed()
        {
            return PartialView("_StudentCounter");
        }


        /*
         * A method that takes an int in as the parameter that will be the Anouncement ID, it will
         * then loop through the Comment Database and find all Comments that are associated with that
         * ID and return it as a List of Comments
         */
        private List<Comment> TableContent(int id)
        {
            var SpecifcCom = (from d in db.Comments
                              where d.CompareFig == id
                              select d).ToList();

            return SpecifcCom;
        }

        //A method that returns a View for Announcements in a ToLists() 
        public ActionResult Index()
        {
            return View(db.Anouncements.ToList());
        }

        //A ActionResult Method that constructs a Partial View and creates the List from the TableContent
        public ActionResult BuildTable(int id)
        {
            return PartialView("_CommentSection", TableContent(id));
        }

        /*
         * A ActionResults that will Display the page that contains the Announcement at the top and Display all the 
         * comments at the Bottom. It takes in a int as a parameter but performs a check to see if there is a int in the 
         * URL. Then uses the HowMany() method to find how many uniqe views there are for this announcement and then using a
         * AnounceDetailView model to display it all on the page
         */
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

            //return PartialView("_StudentCounter", HowMany(anouncement));

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

        //A method to allow us to edit the announcement when we pass the Id into 
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
