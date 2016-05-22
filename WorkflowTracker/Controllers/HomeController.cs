using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using WorkflowTracker.Models;

namespace WorkflowTracker.Controllers
{
    public class HomeController : Controller
    {
        readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            logger.Error("In index Method- Happy Logging!");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Work Flow Tracker";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our Contact";

            return View();
        }

    }
}