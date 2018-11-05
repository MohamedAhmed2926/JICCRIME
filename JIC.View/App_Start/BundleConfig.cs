using System.Web;
using System.Web.Optimization;

namespace JIC.Crime.View
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

            bundles.Add(new ScriptBundle("~/bundles/selectize").Include(
                      "~/Scripts/js/selectize.min.js",
                      "~/Scripts/js/selectize.script.js"));

            bundles.Add(new StyleBundle("~/Content/selectize").Include(
                    "~/Content/css/selectize.bootstrap3.css"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/Sortable").Include(
                    "~/Scripts/js/jquery-sortable.js"
                   ));
            bundles.Add(new ScriptBundle("~/bundles/textcomplete").Include(
                   "~/Scripts/js/textcomplete.js"
                  ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/CourtCss").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/site.css",
                    "~/Content/MvcGrid/mvc-grid.css",
                    "~/Content/font-awesome.css",
                    "~/Content/animate.css",
                    "~/Content/bootstrap.rtl.min.css"
                    //"~/Content/css/styles2.css",
                    //"~/Content/css/jic_style.css"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/customJs").Include(
                    "~/Scripts/MvcGrid/mvc-grid.js",
                    "~/Scripts/ajax-form.js",
                    "~/Scripts/bootstrap-notify.js"));


        }
    }
}
