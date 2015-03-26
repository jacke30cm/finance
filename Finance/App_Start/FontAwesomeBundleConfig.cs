using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Finance.App_Start.FontAwesomeBundleConfig), "RegisterBundles")]

namespace Finance.App_Start
{
	public class FontAwesomeBundleConfig
	{
		public static void RegisterBundles()
		{
			// Add @Styles.Render("~/Content/fontawesome") in the <head/> of your _Layout.cshtml view
			// When <compilation debug="true" />, MVC will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new StyleBundle("~/CssBundle/fontawesome").Include("~/Content/Css/font-awesome.css"));
		}
	}
}