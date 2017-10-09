using System.Web.Optimization;

namespace LiveScoreUpdateSystem.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                       "~/Scripts/Kendo/kendo.web.min.js",
                        "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                    "~/Scripts/FootballTemplate/responsiveslides.min.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/modals").Include(
                "~/Scripts/show-modal.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/fontawesome/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/template-css").Include(
                "~/Content/FootballTemplate/football-template.css"
                ));

            bundles.Add(new StyleBundle("~/Content/Kendo-css").Include(
                  "~/Content/Kendo/kendo.black.min.css",
                  "~/Content/Kendo/kendo.common.min.css"));
        }
    }
}
