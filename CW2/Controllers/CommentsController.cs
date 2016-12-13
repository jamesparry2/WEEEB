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
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /*
         * A method that creates a List of Comments which takes a Comment Object as the Parameter
         * which pulls the information needed from the Comment Object. It then finds the current user
         * and assigns the comment to that users. Saves it the DB and, using a LINQ command, updates 
         * the table with the new information and returns it
         */
        private List<Comment> AjaxMethod(Comment Comment)
        {
            int Compare = Comment.CompareFig;
            string Store = Comment.CommentDes;

            string currentUser = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(
                x => x.Id == currentUser);
            Comment.Student = user;
            Comment.CommentDes = Store;

            db.Comments.Add(Comment);
            db.SaveChanges();

            var Specfic = (from d in db.Comments
                           where d.CompareFig == Compare
                           select d).ToList();

            return Specfic;
        }

        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        /*
         * A standard CRUD created Method from the MVC Framework 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentDes")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                string store = Server.HtmlEncode(comment.CommentDes);
                comment.CommentDes = store;

                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        /*
         * An AJAXMethod which has the Data Annotation Authorize and ensure secuirty with the ValidateAntiForgeryToken 
         * and then Posts it tot the Database. It Binds the information CompareFig and CommentDes to the the Comment #
         * Object
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer,Student")]
        public ActionResult AJAXCreate([Bind(Include = "CompareFig,CommentDes")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                return PartialView("_CommentSection", AjaxMethod(comment));
            }
            return new EmptyResult();
        }


        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentDes,CompareFig")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var CurrentComment = db.Comments.Find(comment.Id);
                CurrentComment.Id = comment.Id;
                CurrentComment.CompareFig = CurrentComment.CompareFig;
                CurrentComment.CommentDes = comment.CommentDes;

                db.Entry(CurrentComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
