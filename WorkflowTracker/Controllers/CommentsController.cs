using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowTracker.Models;

namespace WorkflowTracker.Controllers
{
    public class CommentsController : Controller
    {
        private emahajanDataBaseEntities5 db = new emahajanDataBaseEntities5();

        // GET: Comments
        [Authorize]
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.AspNetUser).Include(c => c.StepDetail);
            return View(comments.ToList());
        }

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.UserID1 = user.Email;
            ViewBag.StepID = new SelectList(db.StepDetails, "StepID", "StepID");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CommentID,UserID,StepID,CommentDate,text,attachment")] Comment comment, int? id, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //Upload File
                if (file != null && file.ContentLength > 0)
                {
                    string path = Server.MapPath("~/Files/" + file.FileName);
                    comment.attachment = file.FileName;
                    file.SaveAs(path);
                }

                // To track user who is commenting
                comment.StepID = id;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                comment.UserID = user.Id;
                comment.CommentDate = DateTime.Now.Date;
                db.Comments.Add(comment);
                db.SaveChanges();
                
                return RedirectToAction("Details", "StepDetails", new { id = comment.StepID });
     
            }

           ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", comment.UserID);
            ViewBag.StepID = new SelectList(db.StepDetails, "StepID", "StepID", comment.StepID);
            return View(comment);
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
