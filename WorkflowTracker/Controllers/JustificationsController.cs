using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowTracker.Models;

namespace WorkflowTracker.Controllers
{
    public class JustificationsController : Controller
    {
        private emahajanDataBaseEntities5 db = new emahajanDataBaseEntities5();

        // GET: Justifications
        [Authorize]
        public ActionResult Index()
        {
            var justifications = db.Justifications.Include(j => j.AspNetUser).Include(j => j.StepDetail);
            return View(justifications.ToList());
        }

        // GET: Justifications/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Justification> justification = db.Justifications.Where(x => x.StepID == id).ToList();
            if (justification == null)
            {
                return HttpNotFound();
            }
            return View(justification);
        }

        // GET: Justifications/Create
        [Authorize]
        public ActionResult Create(int? id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.UserID = user.Email;

           // To track the previous status
            var pre = db.StepDetails.Where(x => x.StepID == id).FirstOrDefault();
            ViewBag.pre = pre.Status;
            ViewBag.StepID = new SelectList(db.StepDetails, "StepID", "StepName");
            return View();
        }

        // POST: Justifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "JustID,UserID,StepID,JustificationDate,JustificationText,PrevStatus,NewStatus")] Justification justification, int? id)
        {
            if (ModelState.IsValid)
            {
                justification.StepID = id;
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                justification.UserID = user.Id;
                justification.JustificationDate = DateTime.Now.Date;
                db.Justifications.Add(justification);
                   
                // To track the new status
                var status = db.StepDetails.Where(x => x.StepID == id).FirstOrDefault();
                status.Status = justification.NewStatus;
                db.SaveChanges();

                return RedirectToAction("Edit", "StepDetails", new { id = justification.StepID });
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "firstName", justification.UserID);
            ViewBag.StepID = new SelectList(db.StepDetails, "StepID", "StepName", justification.StepID);
            return View(justification);
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
