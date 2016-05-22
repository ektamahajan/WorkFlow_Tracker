using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowTracker.Models;
using System.Data;
using System.Data.Entity;


namespace WorkflowTracker.Controllers
{
    public class WelcomeController : Controller
    {

        private emahajanDataBaseEntities5 db = new emahajanDataBaseEntities5();
        // GET: Welcome
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult HistoryReport()
        {
            ViewBag.Message = "History Report";
        
            List<HISTORY_REPORT1> history = db.HISTORY_REPORT1.ToList();
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
         //   return View();
        }

        [Authorize]
        public ActionResult CalendarView()
        {
            return View();
        }

        [Authorize]
        public ActionResult AdminTasks()
        {
            return View();

        }

        // Events for Calendar
        public JsonResult GetEvents()
        {
            var eventList = from p in db.ProjectDetails
                            select new
                            {
                               id = p.ProjectID,
                               title = p.ProjectName,
                               url = "http://localhost:4833/StepDetails/Index/" + p.ProjectID,
                               start = p.CreationDate,
                               end = p.ActCompDate
                            };

            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }


    }
}