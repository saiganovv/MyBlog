using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Comments/

        public ActionResult Index()
        {
            return View(db.CommentModels.ToList());
        }

        //
        // GET: /Comments/Details/5

        public ActionResult Details(int id = 0)
        {
            CommentModel commentmodel = db.CommentModels.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        //
        // GET: /Comments/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Comments/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentModel commentmodel, int id)
        {
            if (ModelState.IsValid)
            {
                commentmodel.UserName = db.UserProfiles.Where(x => x.UserName == User.Identity.Name).First();
                commentmodel.Date = DateTime.Now;
                var post = db.PostModels.Find(id);
                commentmodel.Post = post;
                db.CommentModels.Add(commentmodel);
                db.SaveChanges();
                return RedirectToAction("Index", "Posts");
            }

            return View(commentmodel);
        }

        //
        // GET: /Comments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CommentModel commentmodel = db.CommentModels.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        //
        // POST: /Comments/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Posts");
            }
            return View(commentmodel);
        }

        //
        // GET: /Comments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CommentModel commentmodel = db.CommentModels.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        //
        // POST: /Comments/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentmodel = db.CommentModels.Find(id);
            db.CommentModels.Remove(commentmodel);
            db.SaveChanges();
            return RedirectToAction("Index", "Posts");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}