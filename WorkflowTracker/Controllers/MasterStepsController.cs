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
    public class MasterStepsController : Controller
    {
        private emahajanDataBaseEntities5 db = new emahajanDataBaseEntities5();

        // GET: MasterSteps
        [Authorize]
        public ActionResult Index()
        {
            return View(db.MasterSteps.ToList());
        }

        // GET: MasterSteps/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterStep masterStep = db.MasterSteps.Find(id);
            if (masterStep == null)
            {
                return HttpNotFound();
            }
            return View(masterStep);
        }

        // GET: MasterSteps/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterSteps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "MasterID,MasterStepName,StepStatus")] MasterStep masterStep)
        {
            if (ModelState.IsValid)
            {
                db.MasterSteps.Add(masterStep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(masterStep);
        }

        // GET: MasterSteps/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterStep masterStep = db.MasterSteps.Find(id);
            if (masterStep == null)
            {
                return HttpNotFound();
            }
            return View(masterStep);
        }

        // POST: MasterSteps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "MasterID,MasterStepName,StepStatus")] MasterStep masterStep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterStep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(masterStep);
        }

        // GET: MasterSteps/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterStep masterStep = db.MasterSteps.Find(id);
            if (masterStep == null)
            {
                return HttpNotFound();
            }
            return View(masterStep);
        }

        // POST: MasterSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterStep masterStep = db.MasterSteps.Find(id);
            db.MasterSteps.Remove(masterStep);
            db.SaveChanges();
            return RedirectToAction("Index");
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
