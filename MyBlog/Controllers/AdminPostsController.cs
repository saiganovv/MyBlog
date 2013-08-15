using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminPostsController : Controller
    {
        private UsersContext db = new UsersContext();
        
        // GET: /AdminPosts/Details/5

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
        // GET: /AdminPosts/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminPosts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostModel postmodel)
        {
            if (ModelState.IsValid)
            {
                db.PostModels.Add(postmodel);
                db.SaveChanges();
                return RedirectToAction("Posts", "Admin");
            }

            return View(postmodel);
        }

        //
        // GET: /AdminPosts/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PostModel postmodel = db.PostModels.Find(id);
            //MembershipUserCollection users = Membership.GetAllUsers();
            //ViewBag.AllUsers = users;
            if (postmodel == null)
            {
                return HttpNotFound();
            }
            return View(postmodel);
        }

        //
        // POST: /AdminPosts/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostModel postmodel, int newId)
        {
            //var newUser = db.UserProfiles.Find(newId);
            //var oldUser = db.UserProfiles.Find(postmodel.User.UserId);
            //if (newId != postmodel.User.UserId)
            //{
            //    oldUser.UserPosts.Remove(postmodel);
            //    newUser.UserPosts.Add(postmodel);
            //    db.SaveChanges();
            //    return RedirectToAction("Posts", "Admin");
            //}
            if (ModelState.IsValid){
                //postmodel.User = newUser;
                db.Entry(postmodel).State = EntityState.Modified;
                db.SaveChanges();
                //newUser.UserPosts.Add(postmodel);
                db.SaveChanges();
                return RedirectToAction("Posts", "Admin");
            }
            return View(postmodel);
        }

        //
        // GET: /AdminPosts/Delete/5

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
        // POST: /AdminPosts/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostModel postmodel = db.PostModels.Find(id);
            db.PostModels.Remove(postmodel);
            db.SaveChanges();
            return RedirectToAction("Posts", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}