using System.Web;
using System.Web.Optimization;

namespace SRS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/wow.min.js",
                        "~/Scripts/DataTables/jquery.datatables.js",
                        "~/Scripts/DataTables/datatables.bootstrap.js",
                        "~/Scripts/main.js",
                        "~/Scripts/custom.min.js",
                        "~/Scripts/typeahead.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/CSS").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/custom.min.css",
                      "~/Content/animate.css",
                      "~/Content/hover.css",
                      "~/Content/myCSS.css",
                      "~/Content/site.css",
                      "~/Content/Typeahead.css",
                      "~/Content/DataTables/css/datatables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
        }
    }
}
