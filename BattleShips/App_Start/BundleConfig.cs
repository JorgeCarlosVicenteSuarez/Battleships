namespace BattleShips
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/angular-material.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular/angular.js",
                      "~/Scripts/angular-animate/angular-animate.js",
                      "~/Scripts/angular-aria/angular-aria.js",
                      "~/Scripts/angular-material/angular-material.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/battleships").Include(
                      "~/App/battleships.js",
                      "~/App/Services/*.js",
                      "~/App/Controllers/*.js",
                      "~/App/Directives/*.js"));
        }
    }
}
