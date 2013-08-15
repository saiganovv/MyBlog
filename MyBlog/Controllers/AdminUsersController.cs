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
    public class AdminUsersController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /AdminUsers/

        public ActionResult Index()
        {
            return View(db.UserProfiles.ToList());
        }

        //
        // GET: /AdminUsers/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /AdminUsers/Create

        public ActionResult Create()
        {
            ViewData["roles"] = new[]
            {
                new SelectListItem
                {
                    Text = "admin",
                    Value = "admin"
                },
                new SelectListItem
                {
                    Text = "user",
                    Value = "user"
                },
                new SelectListItem
                {
                    Text = "writer",
                    Value = "writer"
                }

            };
            return View();
        }

        //
        // POST: /AdminUsers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile userprofile, dynamic roles)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                Roles.AddUserToRole(userprofile.UserName, roles[0]);
                return RedirectToAction("Index");
            }

            return View(userprofile);
        }

        //
        // GET: /AdminUsers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewData["roles"] = new[] {
                new SelectListItem
                {
                    Text = "admin",
                    Value = "admin"
                }, 
                new SelectListItem
                {
                    Text = "user",
                    Value = "user"
                }, 
                new SelectListItem
                {
                    Text = "writer",
                    Value = "writer"
                } 
            
            };
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /AdminUsers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile, dynamic roles)
        {
            if (ModelState.IsValid)
            {
                var currentRole = Roles.GetRolesForUser(userprofile.UserName)[0];
                if ((string)currentRole != roles[0])
                {
                    Roles.RemoveUserFromRole(userprofile.UserName, currentRole);
                    Roles.AddUserToRole(userprofile.UserName, roles[0]);
                }
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // GET: /AdminUsers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /AdminUsers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
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