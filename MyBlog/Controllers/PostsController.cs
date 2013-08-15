using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Posts/

        public ActionResult Index()
        {
            var postLINQ = from pt in db.PostModels
                           where pt.Published == true
                           orderby pt.CurrentDate descending 
                           select pt;
            return View(postLINQ.ToList());
        }

        //
        // GET: /Posts/Details/5

        public ActionResult Details(int id = 0)
        {
            PostModel postmodel = db.PostModels.Find(id);
            if (postmodel == null)
            {
                return HttpNotFound();
            }
            return View(postmodel);
        }

        //
        // GET: /Posts/Create
        [Authorize(Roles = "admin, writer")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Posts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostModel postmodel)
        {
            if (ModelState.IsValid)
            {
                postmodel.User = db.UserProfiles.Where(x => x.UserName == User.Identity.Name).First();
                postmodel.CurrentDate = DateTime.Now;
                db.PostModels.Add(postmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postmodel);
        }

        //
        // GET: /Posts/Edit/5
        [Authorize(Roles = "admin, writer")]
        public ActionResult Edit(int id = 0)
        {
            PostModel postmodel = db.PostModels.Find(id);
            if (postmodel.User.UserName != User.Identity.Name)
            {
                if (User.IsInRole("admin"))
                {
                    return View(postmodel);
                }
                else
                {
                    return RedirectToActionPermanent("Index", "Posts");
                }
            }
            if (postmodel == null)
            {
                return HttpNotFound();
            }
            return View(postmodel);
        }

        //
        // POST: /Posts/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostModel postmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postmodel);
        }

        //
        // GET: /Posts/Delete/5
        [Authorize(Roles = "admin, writer")]
        public ActionResult Delete(int id = 0)
        {
            PostModel postmodel = db.PostModels.Find(id);
            if (postmodel == null)
            {
                return HttpNotFound();
            }
            return View(postmodel);
        }

        //
        // POST: /Posts/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostModel postmodel = db.PostModels.Find(id);
            var commentsLINQ = from c in db.CommentModels
                               where c.Post.Id == postmodel.Id
                               select c;

            foreach (var comment in commentsLINQ)
            {
                CommentModel cm = db.CommentModels.Find(comment.Id);
                db.CommentModels.Remove(cm);
            }

            db.PostModels.Remove(postmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}