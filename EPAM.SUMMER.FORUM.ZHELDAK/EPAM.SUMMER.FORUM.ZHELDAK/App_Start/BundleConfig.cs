using System.Web;
using System.Web.Optimization;
using EPAM.SUMMER.FORUM.ZHELDAK.Controllers;

namespace EPAM.SUMMER.FORUM.ZHELDAK
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include("~/Scripts/site.js", "~/Scripts/jquery.unobtrusive-ajax.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include("~/Script/autocomplete.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js", "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css", "~/Content/bootstrap-multiselect.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                "~/Content/jquery-ui.css"));

           bundles.Add(new StyleBundle("~/Content/account").Include("~/Content/account.css"));
        }
    }
}
