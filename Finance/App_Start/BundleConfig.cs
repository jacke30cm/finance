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
                        "~/Scripts/spin.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/master").Include(
                        "~/Scripts/Master/master.js",
                        "~/Scripts/Master/control-panel.js"));


            //STYLES

            bundles.Add(new StyleBundle("~/CssBundle/css").Include(
                      "~/Content/Css/Main.css",
                      "~/Content/Css/Menu.css",
                      "~/Content/Css/jquery.mCustomScrollbar.css",
                      "~/Content/Css/Content.css"));
        }
    }
}
