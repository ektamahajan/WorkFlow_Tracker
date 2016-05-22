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
    public class StepDetailsController : Controller
    {
        private emahajanDataBaseEntities5 db = new emahajanDataBaseEntities5();


        [Authorize]
        public ActionResult CommentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Comment> comment = db.Comments.Where(x => x.StepID == id).ToList();

            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }


        [Authorize]
        // GET: StepDetails
        public ActionResult Index(int? id)
        {

            var projectID = from p in db.ProjectDetails
                            where p.ProjectID == id
                            select p.ProjectID;

            var stepList = from stepdetail in db.StepDetails
                           where stepdetail.ProjectID == projectID.FirstOrDefault()
                           select stepdetail;

            return View(stepList);
        }

        [Authorize]
        // GET: StepDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StepDetail stepDetail = db.StepDetails.Find(id);
            if (stepDetail == null)
            {
                return HttpNotFound();
            }
            return View(stepDetail);
        }

        [Authorize]
        // GET: StepDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StepDetail stepDetail = db.StepDetails.Find(id);

            if (stepDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.ProjectDetails, "ProjectID", "ProjectName", stepDetail.ProjectID);
            return View(stepDetail);
        }

        // POST: StepDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "StepID,ProjectID,EstStartDate,EstEndDate,ActStartDate,ActEndDate,StepName,Status")] StepDetail stepDetail)
        {
            if (ModelState.IsValid)
            {

                // For automatic Progress
                var StepsCount = db.StepDetails.Where(x => x.ProjectID == stepDetail.ProjectID).Count();
                var CompletedStepsCount = db.StepDetails.Where(x => x.ProjectID == stepDetail.ProjectID && x.Status == "Completed").Count();

                double ProjectProgress = (((Double)CompletedStepsCount / (Double)StepsCount) * 100);
                Double ProgressDec = Math.Round(ProjectProgress, 2);

                ProjectDetail progress1 = db.ProjectDetails.Where(x => x.ProjectID == stepDetail.ProjectID).FirstOrDefault();
                progress1.Progress = (int)ProgressDec;

                stepDetail.Status = stepDetail.Status;
                
                // For Date Shift functionality
                StepDetail step = db.StepDetails.Single(x => x.StepID == stepDetail.StepID);
                step.ActStartDate = stepDetail.ActStartDate;
                step.EstStartDate = stepDetail.EstStartDate;

                if (step.EstEndDate != null && stepDetail.EstEndDate != null)
                {
                    
                    DateTime PrevEstEndDate = step.EstEndDate.Value;
                    DateTime NewEstEndDate = stepDetail.EstEndDate.Value;

                    double StepDateDiff = (NewEstEndDate.Date - PrevEstEndDate.Date).Days;

                    List<StepDetail> Steplist = db.StepDetails.Where(x => x.ProjectID == step.ProjectID).ToList();
                    step.EstEndDate = stepDetail.EstEndDate;
                    foreach (StepDetail item in Steplist)
                    {
                        if (item.StepID != step.StepID)
                        {
                            if (item.EstEndDate != null)
                            {
                                item.EstEndDate = item.EstEndDate.Value.AddDays(StepDateDiff);
                            }
                        }
                    }
                }
                else
                {
                    step.EstEndDate = stepDetail.EstEndDate;
                }


                if (step.ActEndDate != null && stepDetail.ActEndDate != null)
                {
                    DateTime NewActEndDate = stepDetail.ActEndDate.Value;
                    DateTime PrevActEndDate = step.ActEndDate.Value;

                    int StepDateDiff = (NewActEndDate.Date - PrevActEndDate.Date).Days;

                    List<StepDetail> Steplist = db.StepDetails.Where(x => x.ProjectID == step.ProjectID).ToList();
                    step.ActEndDate = stepDetail.ActEndDate;

                    foreach (StepDetail item in Steplist)
                    {
                        if (item.StepID != step.StepID) 
                        {
                            if (item.ActEndDate != null)
                            {
                                item.ActEndDate = item.ActEndDate.Value.AddDays(StepDateDiff);
                            }
                        }
                    }
                }
                else
                {
                    step.ActEndDate = stepDetail.ActEndDate;
                }

                db.Entry(step).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","StepDetails",new { id = stepDetail.ProjectID });
            }
            ViewBag.ProjectID = new SelectList(db.ProjectDetails, "ProjectID", "ProjectName", stepDetail.ProjectID);
            return View(stepDetail);
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
