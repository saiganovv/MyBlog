using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        UsersContext db = new UsersContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts()
        {
            var posts = from pt in db.PostModels
                        orderby pt.Published ascending
                        select pt;

            return View(posts.ToList());
        }

        public ActionResult Users()
        {
            var users = db.UserProfiles.ToList();

            return View(users);
        }

        public ActionResult Comments()
        {
            var comments = db.CommentModels.ToList();

            return View(comments);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
