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
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace WorkflowTracker.Controllers
{
    public class ProjectDetailsController : Controller
    {
        private emahajanDataBaseEntities5 db = new emahajanDataBaseEntities5();


        // GET: ProjectDetails
        [Authorize]
        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            
                if (System.Web.HttpContext.Current.User.IsInRole("User"))
                {

                var userID = db.AspNetUsers.Where(x => x.Id.Equals(user.Id)).FirstOrDefault();
                var projectId = db.UserProjects.Where(x => x.UserID.Equals(userID.Id)).ToList();
                 IList<ProjectDetail> projectList = new List<ProjectDetail>();

                foreach (var item in projectId)
                {
                     var project = db.ProjectDetails.Where(x => x.ProjectID == item.ProjectID).FirstOrDefault();
                    projectList.Add(project);
                }

                return View(projectList);
            }
            else
            {
                var projectDetails = db.ProjectDetails.Include(p => p.AspNetUser);
                return View(projectDetails.ToList());
            }

        }

        // GET: ProjectDetails/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectDetail projectDetail = db.ProjectDetails.Find(id);
            if (projectDetail == null)
            {
                return HttpNotFound();
            }
            return View(projectDetail);
        }

        // GET: ProjectDetails/Create
        [Authorize]
        public ActionResult Create()
        {
           ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.UserID = user.Email;
            ViewBag.List = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }


        // POST: ProjectDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,UserID,Status,Progress,CreationDate,EstOrgCompDate,EstCurrCompDate,ActCompDate")] ProjectDetail projectDetail)
        {
 
            if (ModelState.IsValid)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                projectDetail.UserID = user.Id;
                projectDetail.CreationDate = DateTime.Now.Date;
                db.ProjectDetails.Add(projectDetail);
                db.SaveChanges();
                var users = Request.Form["List"];

                UserProject proj = new UserProject();
                if (users != null)
                {
                    var user1 = users.Split(',');
                    foreach (var item in user1)
                    {
                        proj.UserID = item;
                        proj.ProjectID = projectDetail.ProjectID;
                        db.UserProjects.Add(proj);
                        db.SaveChanges();
                    }
                }

                // To create automatic steps
                IEnumerable<string> query = from p in db.MasterSteps
                                            where p.StepStatus == true
                                            select p.MasterStepName;

                    StepDetail sd = new StepDetail();
                    foreach (string step in query.ToList())
                    {
                        sd.ProjectID = projectDetail.ProjectID;
                        sd.StepName = step;
                        sd.Status = "Planned";                
                        db.StepDetails.Add(sd);
                        db.SaveChanges();
                    }
              
                return RedirectToAction("Index");
            }
            return View(projectDetail);
        }

        // GET: ProjectDetails/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectDetail projectDetail = db.ProjectDetails.Find(id);
            if (projectDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", projectDetail.UserID);
            return View(projectDetail);
        }

        // POST: ProjectDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,UserID,Status,Progress,CreationDate,EstOrgCompDate,EstCurrCompDate,ActCompDate")] ProjectDetail projectDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", projectDetail.UserID);
            return View(projectDetail);
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
