using System.Web;
using System.Web.Optimization;

namespace Finance
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //SCRIPTS

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.3.min.js",
                        "~/Scripts/jqueryEase.js",
                         "~/Scripts/jquery.mousewheel.min.js",
                        "~/Scripts/jquery.mCustomScrollbar.js",
                        "~/Scripts/jquery.easypiechart.min.js",
                        "~/Scripts/jquery.flot.min.js",
                        "~/Scripts/jquery.flot.pie.min.js",
                        "~/Scripts/jquery-labelauty.js",
                        "~/Scripts/spin.min.js"));

            // Master-js
            bundles.Add(new ScriptBundle("~/bundles/master").Include(
                        "~/Scripts/Master/master.js",
                        "~/Scripts/Master/control-panel.js"));

                                    
            // Anchor-routes for sites that handle anchor-routing
            bundles.Add(new ScriptBundle("~/bundles/anchor-routing").Include(
                        "~/Scripts/Master/anchor-route.js"));

            // Custody-account
            bundles.Add(new ScriptBundle("~/bundles/custody-account").Include(
                        "~/Scripts/Contest/custody-account.js"));






            //STYLES

            bundles.Add(new StyleBundle("~/CssBundle/css").Include(
                      "~/Content/Css/Main.css",
                      "~/Content/Css/Menu.css",
                      "~/Content/Css/jquery.mCustomScrollbar.css",
                      "~/Content/Css/jquery-labelauty.css",
                      "~/Content/Css/Components.css"));

            //Home
            bundles.Add(new StyleBundle("~/CssBundle/home").Include(
                      "~/Content/Css/Home.css"));


            //About
            bundles.Add(new StyleBundle("~/CssBundle/about").Include(
                      "~/Content/Css/About.css"));

            //Custody Account -> Default startpage when in contest
            bundles.Add(new StyleBundle("~/CssBundle/custody-account").Include(
                      "~/Content/Css/Custody-Account.css"));
        }
    }
}
