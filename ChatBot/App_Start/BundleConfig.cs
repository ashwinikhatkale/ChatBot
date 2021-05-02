using System.Web;
using System.Web.Optimization;

namespace ChatBot
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));           

            bundles.Add(new ScriptBundle("~/bundles/questions").Include(
                   "~/Scripts/jquery-3.4.1.js",
                   "~/Scripts/questions.js"));

            bundles.Add(new ScriptBundle("~/bundles/student-questions").Include(
                  "~/Scripts/jquery-3.4.1.js",
                  "~/Scripts/student-questions.js"));

            bundles.Add(new ScriptBundle("~/bundles/notices").Include(
                  "~/Scripts/jquery-3.4.1.js",
                  "~/Scripts/notices.js"));

            bundles.Add(new StyleBundle("~/Content/table").Include(
                    "~/Content/table.css"));

            bundles.Add(new StyleBundle("~/Content/add-notice").Include(
                  "~/Content/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/add-notice").Include(
                  "~/Scripts/moment.js",
                   "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/jquery-3.4.1.js",
                      "~/Scripts/add-notice.js"));

            bundles.Add(new StyleBundle("~/Content/noticeboard").Include(
                "~/Content/bootstrap-datetimepicker.min.css",
                 "~/Content/table.css"));

            bundles.Add(new ScriptBundle("~/bundles/noticeboard").Include(
                 "~/Scripts/moment.js",
                  "~/Scripts/bootstrap-datetimepicker.min.js",
                     "~/Scripts/jquery-3.4.1.js",
                     "~/Scripts/noticeboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/chatbot").Include(
                 "~/Scripts/jquery-3.4.1.js",
                 "~/Scripts/jquery-ui.js",
                 "~/Scripts/chatbot.js"));
        }
    }
}
 