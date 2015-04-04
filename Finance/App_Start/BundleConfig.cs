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
                        "~/Scripts/jquery-easy-list-splitter.js",
                        "~/Scripts/spin.min.js"));

            // Master-js
            bundles.Add(new ScriptBundle("~/bundles/master").Include(
                        "~/Scripts/Master/spin-configuration.js",
                         "~/Scripts/Master/master.js",
                        "~/Scripts/Master/control-panel.js"));

                                    
            // Anchor-routes for sites that handle anchor-routing
            bundles.Add(new ScriptBundle("~/bundles/anchor-routing").Include(
                        "~/Scripts/Master/anchor-route.js"));

            // Portfolio
            bundles.Add(new ScriptBundle("~/bundles/portfolio").Include(
                        "~/Scripts/Contest/portfolio.js"));

            // Market
            bundles.Add(new ScriptBundle("~/bundles/market").Include(
                       "~/Scripts/Contest/market.js"));

            // SignUp
            bundles.Add(new ScriptBundle("~/bundles/sign-up").Include(
                       "~/Scripts/Signup/start.js"));


            //STYLES

            bundles.Add(new StyleBundle("~/CssBundle/css").Include(
                      "~/Content/Css/Main.css",
                      "~/Content/Css/Menu.css",
                      "~/Content/Css/jquery.mCustomScrollbar.css",
                      "~/Content/Css/jquery-labelauty.css",
                      "~/Content/Css/Components.css"));

            //Front-page
            bundles.Add(new StyleBundle("~/CssBundle/front-page").Include(
                      "~/Content/Css/Front-Page.css"));

            //Home
            bundles.Add(new StyleBundle("~/CssBundle/home").Include(
                      "~/Content/Css/Home.css"));


            //About
            bundles.Add(new StyleBundle("~/CssBundle/about").Include(
                      "~/Content/Css/About.css"));

            //Portfolio
            bundles.Add(new StyleBundle("~/CssBundle/portfolio").Include(
                      "~/Content/Css/Portfolio.css"));

            //Market
            bundles.Add(new StyleBundle("~/CssBundle/market").Include(
                      "~/Content/Css/Market.css"));
        }
    }
}
