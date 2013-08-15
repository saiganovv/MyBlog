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
    public class AdminCommentsController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /AdminComments/

        public ActionResult Index()
        {
            return View(db.CommentModels.ToList());
        }

        //
        // GET: /AdminComments/Details/5

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
        // GET: /AdminComments/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminComments/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                db.CommentModels.Add(commentmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentmodel);
        }

        //
        // GET: /AdminComments/Edit/5

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
        // POST: /AdminComments/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentmodel);
        }

        //
        // GET: /AdminComments/Delete/5

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
        // POST: /AdminComments/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentmodel = db.CommentModels.Find(id);
            db.CommentModels.Remove(commentmodel);
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