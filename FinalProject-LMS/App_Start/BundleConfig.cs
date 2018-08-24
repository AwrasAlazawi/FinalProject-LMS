using System.Web.Optimization;

namespace FinalProject_LMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/message").Include(
                    "~/Scripts/Message.js"));
            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                     "~/Scripts/Search.js"));
            bundles.Add(new ScriptBundle("~/bundles/roles").Include(
                      "~/Scripts/Roles.js"));
            bundles.Add(new ScriptBundle("~/bundles/datevalidations").Include(
                       "~/Scripts/DateValidations.js"));
            bundles.Add(new ScriptBundle("~/bundles/coursenamevalidations").Include(
                      "~/Scripts/CourseNameValidations.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
