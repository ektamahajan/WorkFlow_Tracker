using System.Web;
using System.Web.Optimization;

namespace WorkflowTracker
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/DatePickerReady.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/bootstrap-datepicker3.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fullcalendarcss").Include(
                       "~/Content/themes/jquery.ui.all.css",
                      "~/Content/fullcalendar.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendarjs").Include(
                 "~/Scripts/jquery.ui.{version}.min.js",
                 "~/Scripts/moment.min.js",
                 "~/Scripts/fullcalendar.min.js"
          ));

            bundles.Add(new ScriptBundle("~/bundles/datePicker").Include(
          "~/Scripts/moment.min.js",
          "~/Scripts/bootstrap-datepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                     "~/Content/bootstrap-datepicker.min.css"));
        }
    }
}
