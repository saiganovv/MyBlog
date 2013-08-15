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
    public class PostsController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Posts/

        public ActionResult Index()
        {
            return View(db.PostModels.ToList());
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
                postmodel.UserName = User.Identity.Name;
                postmodel.CurrentDate = DateTime.Now.Date;
                db.PostModels.Add(postmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postmodel);
        }

        //
        // GET: /Posts/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PostModel postmodel = db.PostModels.Find(id);
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