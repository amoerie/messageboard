using System.Web;
using System.Web.Optimization;

namespace Messageboard.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/lib/jQuery/jquery-{version}.js",
                "~/Scripts/lib/Bootstrap/bootstrap-{version}.js",
                "~/Scripts/lib/AngularJs/angular-{version}.js",
                "~/Scripts/lib/AngularJs/angular-route-{version}.js",
                "~/Scripts/lib/AngularJs/angular-resource-{version}.js",
                "~/Scripts/lib/AngularJs/angular.nl.js",
                "~/Scripts/lib/Moment/moment-{version}.js",
                "~/Scripts/lib/UnderscoreJs/underscore-{version}.js",
                "~/Scripts/lib/Alertify/alertify-{version}.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/app.js",
                "~/Scripts/app/services/*.js",
                "~/Scripts/app/controllers/*.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/lib/Modernizr/modernizr-*"));

            #endregion

            #region Styles

            bundles.Add(new StyleBundle("~/Styles/lib/Bootstrap/css").Include(
                "~/Styles/lib/Bootstrap/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Styles/lib/FontAwesome/css").Include(
                "~/Styles/lib/FontAwesome/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Styles/lib/Alertify/css").Include(
                "~/Styles/lib/Alertify/alertify.core.css",
                "~/Styles/lib/Alertify/alertify.default.css"));
            
            bundles.Add(new StyleBundle("~/Styles/app/css").Include(
                "~/Styles/app/layout.css"));

            #endregion
        }
    }
}